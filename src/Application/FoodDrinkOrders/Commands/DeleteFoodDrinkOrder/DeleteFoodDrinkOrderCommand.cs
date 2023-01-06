using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkOrders.Commands.DeleteFoodDrinkOrder
{
    public record DeleteFoodDrinkOrderCommand(int Id) : IRequest;

    public class DeleteFoodDrinkOrderCommandHandler : IRequestHandler<DeleteFoodDrinkOrderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFoodDrinkOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFoodDrinkOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FoodDrinkOrders
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(FoodDrinkOrders), request.Id);
            }

            _context.FoodDrinkOrders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}