using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Commands.DeleteFoodDrinkMenuCommand
{
    public record DeleteFoodDrinkMenuCommand(int Id) : IRequest;

    public class DeleteFoodDrinkMenuCommandHandler : IRequestHandler<DeleteFoodDrinkMenuCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFoodDrinkMenuCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFoodDrinkMenuCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FoodDrinkMenus
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if( entity == null)
            {
                throw new NotFoundException(nameof(FoodDrinkMenus), request.Id);
            }

            _context.FoodDrinkMenus.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;    
        }
    }
}