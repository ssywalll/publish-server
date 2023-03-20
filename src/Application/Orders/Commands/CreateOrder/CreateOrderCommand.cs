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
using AutoMapper.QueryableExtensions;

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

            var cartUser = await _context.Carts
                .Where(x => x.User_Id.Equals(tokenInfo.Owner_Id) && x.IsChecked)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            cartUser.ForEach(x =>
            {
                var foodDrinkOrder = new FoodDrinkOrder
                {
                    Food_Drink_Id = x.Food_Drink_Id,
                    Order_Id = entity.Id,
                    Quantity = x.Quantity,
                };

                _context.FoodDrinkOrders.Add(foodDrinkOrder);

                _context.Carts.Remove(x);
            });

            var createdOrder = await _context.Orders
                .FindAsync(new object[] { entity.Id }, cancellationToken);

            if (request.OrderData.Payment!.ImageValidate() is false)
                throw new NotFoundException("Ekstensi berkas bukan merupkan ekstensi gambar yang diperbolehkan", HttpStatusCode.BadRequest);

            if (request.OrderData.Payment!.SizeValidate() is false)
                throw new NotFoundException("Ukuran berkas melebihi 2MB", HttpStatusCode.BadRequest);

            var fileExtension = Path.GetExtension(request.OrderData.Payment!.FileName);
            var dateName = entity.Order_Time!.ToString("yyyy-MM-dd");
            var imageName = $"{entity.Id}-{entity.User_Id}-payment-{dateName}{fileExtension}";

            var imageFullPath = imageName.GetFullPath("payment");

            using (var stream = File.Create(imageFullPath))
            {
                await request.OrderData.Payment!.CopyToAsync(stream, cancellationToken);
            }

            createdOrder!.Payment_Url = Path.Combine("payment", imageName);

            await _context.SaveChangesAsync(cancellationToken);

            return new PostOrderVm
            {
                Status = "Ok",
                Data = entity.Id
            };
        }
    }
}