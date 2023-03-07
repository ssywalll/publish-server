using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Common.Exceptions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CleanArchitecture.Application.CreateOrders.Commands.CreateOrder
{
    public record CreateOrderCommand : UseAprizax, IRequest<PostOrderVm>
    {
        public PostOrderDto? OrderData { get; init; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, PostOrderVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostOrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var bankUser = await _context.BankAccounts
                .Where(x => x.UserId.Equals(tokenInfo.Owner_Id) && x.IsChoosen)
                .SingleAsync(cancellationToken);

            // var imageKit = request.GetImagekit();

            var entity = new Order
            {
                Meal_Date = request.OrderData!.MealDate,
                Address = request.OrderData!.Address,
                User_Id = tokenInfo.Owner_Id ?? 0,
                BankAccount_Id = bankUser.Id,
                Bank_Number = bankUser.BankNumber,
            };

            if (entity is null)
                throw new NotFoundException("Order gagal dibuat", HttpStatusCode.BadRequest);

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // var requestedFile = new FileCreateRequest
            // {
            //     file = request.OrderData!.PaymentScreenshoot,
            //     fileName = $"{entity.Id}-{entity.User_Id}-payment"
            // };

            // var uploadedFile = await imageKit.Path("/payment").UploadAsync(requestedFile);

            var cartUser = await _context.Carts
                .Where(x => x.User_Id.Equals(tokenInfo.Owner_Id) && x.IsChecked)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            cartUser.ForEach(x =>
            {
                var foodDrinkOrder = new FoodDrinkOrder
                {
                    Food_Drink_Id = x.Food_Drink_Id,
                    Order_Id = entity!.Id,
                    Quantity = x.Quantity,
                };

                _context.FoodDrinkOrders.Add(foodDrinkOrder);

                _context.Carts.Remove(x);
            });

            // var order = await _context.Orders
            //     .FindAsync(entity, cancellationToken);

            // order!.Payment_Url = uploadedFile.filePath;

            await _context.SaveChangesAsync(cancellationToken);

            var orderData = new OrderVmDto
            {
                OrderTime = entity!.Order_Time,
                MealDate = entity!.Meal_Date,
                PaymentUrl = entity!.Payment_Url,
                Address = entity.Address,
                BankNumber = entity.Bank_Number,
                UserName = await _context.Users
                    .Where(x => x.Id.Equals(entity.User_Id))
                    .AsNoTracking()
                    .Select(y => y.Name)
                    .SingleAsync(cancellationToken)
            };

            return new PostOrderVm
            {
                Status = "Ok",
                OrdersData = orderData
            };
        }
    }
}