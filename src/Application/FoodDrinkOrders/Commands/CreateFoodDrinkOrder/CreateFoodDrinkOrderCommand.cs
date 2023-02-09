using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.FoodDrinkOrders.Commands.CreateFoodDrinkOrder
{
    public record CreateFoodDrinkOrderCommand : IRequest<FoodDrinkOrder>
    {
        public int Food_Drink_Id { get; init; }
        public int Order_Id { get; init; }
        public int Quantity { get; init; }
    } 

    public class CreateFoodDrinkOrderCommandHandler : IRequestHandler<CreateFoodDrinkOrderCommand, FoodDrinkOrder>
    {
        private readonly IApplicationDbContext _context;

        public CreateFoodDrinkOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FoodDrinkOrder> Handle(CreateFoodDrinkOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new FoodDrinkOrder
            {
                Food_Drink_Id = request.Food_Drink_Id,
                Order_Id = request.Order_Id,
                Quantity = request.Quantity
            };

            _context.FoodDrinkOrders.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}