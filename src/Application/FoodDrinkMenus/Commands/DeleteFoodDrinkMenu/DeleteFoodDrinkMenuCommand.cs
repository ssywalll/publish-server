using CleanArchitecture.Application.Common.Context;
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
            var target = await _context.FoodDrinkMenus.SingleOrDefaultAsync(
                item => item.Id.Equals(request.Id)
            );
            if (target is null)
                throw new NotFoundException(nameof(FoodDrinkMenus), request.Id);

            File.Delete(target.Image_Url.GetFullPath());

            _context.FoodDrinkMenus.Remove(target);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}