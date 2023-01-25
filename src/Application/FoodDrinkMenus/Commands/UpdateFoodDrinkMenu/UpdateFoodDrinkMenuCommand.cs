using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;

namespace CleanArchitecture.Application.FoodDrinkMenus.Commands.UpdateFoodDrinkMenu
{
    public record UpdateFoodDrinkMenuCommand : IRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public float Price { get; init; }
        public int Min_Order { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Image_Url { get; init; } = string.Empty;
    }

    public class UpdateFoodDrinkMenuCommandHandler : IRequestHandler<UpdateFoodDrinkMenuCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFoodDrinkMenuCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateFoodDrinkMenuCommand request, CancellationToken cancellationToken)
        {
            var target = await _context.FoodDrinkMenus
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (target is null)
                throw new NotFoundException(nameof(FoodDrinkMenus), request.Id);
            _context.FoodDrinkMenus.Update(target);

            target.Name = request.Name;
            target.Price = request.Price;
            target.Min_Order = request.Min_Order;
            target.Description = request.Description;
            target.Image_Url = request.Image_Url;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}