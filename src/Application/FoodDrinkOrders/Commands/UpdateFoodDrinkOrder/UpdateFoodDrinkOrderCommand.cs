using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.FoodDrinkOrders.Commands.UpdateFoodDrinkOrder
{
    public record UpdateFoodDrinkOrderCommand : IRequest
    {
        public int Id { get; init; }
        public int Food_Drink_Id { get; init; }
        public int Order_Number { get; init; }
    }

    public class UpdateFoodDrinkOrderCommandHandler : IRequestHandler<UpdateFoodDrinkOrderCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFoodDrinkOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFoodDrinkOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FoodDrinkOrders
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FoodDrinkOrders), request.Id);
            }

            entity.Food_Drink_Id = request.Food_Drink_Id;
            entity.Order_Number = request.Order_Number;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}