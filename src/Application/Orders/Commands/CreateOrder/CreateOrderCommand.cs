using CleanArchitecture.Application.Common.Interfaces;
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
    public record CreateOrderCommand : IRequest<PostOrderVm>
    {
        public string? Token { get; init;}
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
            if (request == null)
                return null!;

            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = secretKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                var entity = new Order
                    {
                        Meal_Date = request.OrderData!.Meal_Date,
                        Address = request.OrderData!.Address,
                        User_Id = userId,
                        BankAccount_Id = request.OrderData!.BankAccount_Id
                    };

                if(entity != null)
                {
                    _context.Orders.Add(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var userData = await _context.Users
                    .Where(x => x.Id == entity!.User_Id)
                    .SingleOrDefaultAsync(cancellationToken);
             
                var cartsData = await _context.Carts
                    .Where(x => x.User_Id == userData!.Id)
                    .Where(y => y.IsChecked == true)
                    .ToListAsync(cancellationToken);

                foreach(var item in cartsData)
                {
                    var orderData = new FoodDrinkOrder
                    {
                        Food_Drink_Id = item.Food_Drink_Id,
                        Order_Id = entity!.Id,
                        Quantity = item.Quantity
                    };

                    _context.FoodDrinkOrders.Add(orderData);
                    await _context.SaveChangesAsync(cancellationToken);
                }


                return new PostOrderVm
                {
                    Status = "Ok",
                    OrdersData =  new OrderVmDto
                    {
                        Order_Time = entity!.Order_Time,
                        Meal_Date = entity!.Meal_Date,
                        BankAccount_Id = entity.BankAccount_Id,
                        Payment_Url = entity.Payment_Url,
                        Address = entity.Address,
                        User_Name = userData!.Name,
                        Bank_Number = request.OrderData.Bank_Number
                    }
                };
            }
            catch
            {
                throw new NotFoundException("Token Tidak Ada Harap Login Kembali", HttpStatusCode.BadRequest);
            }
        }
    }
}