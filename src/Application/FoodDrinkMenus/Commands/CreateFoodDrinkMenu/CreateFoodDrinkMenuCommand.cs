using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.FoodDrinkMenus.Commands.CreateFoodDrinkMenu
{
    public record CreateFoodDrinkMenuCommand : IRequest<FoodDrinkMenu>
    {
        public string Name { get; init; } = string.Empty;
        public float Price { get; init; }
        public int Min_Order { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Image_Url { get; init; } = string.Empty;
    }
    public class CreateFoodDrinkMenuCommandHandler : IRequestHandler<CreateFoodDrinkMenuCommand, FoodDrinkMenu>
    {
        private readonly IApplicationDbContext _context;

        public CreateFoodDrinkMenuCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FoodDrinkMenu> Handle(CreateFoodDrinkMenuCommand request, CancellationToken cancellationToken)
        {
            var entity = new FoodDrinkMenu
            {
                Name = request.Name,
                Price = request.Price,
                Min_Order = request.Min_Order,
                Description = request.Description,
                Image_Url = request.Image_Url
            };

            return await Aprizax.Insert<FoodDrinkMenu>
            (_context, _context.FoodDrinkMenus, entity, cancellationToken);
            //konteks, tabel konteks, record baru, cancelationToken
        }
    }
}