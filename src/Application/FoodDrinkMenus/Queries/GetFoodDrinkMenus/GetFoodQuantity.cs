using System.Net;
using AutoMapper;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record FoodDrinkQuantity
    {
        public int FoodQuantity { get; init; }
        public int DrinkQuantity { get; init; }
    }

    public record GetFoodDrinkQuantityVm
    {
        public string? Status { get; init; }
        public FoodDrinkQuantity? Data { get; init; }
    }

    public record GetFoodDrinkQuantity : UseAprizax, IRequest<GetFoodDrinkQuantityVm>;

    public record GetFoodDrinkQuantityHandler : IRequestHandler<GetFoodDrinkQuantity, GetFoodDrinkQuantityVm>
    {
        private readonly IApplicationDbContext _context;
        public GetFoodDrinkQuantityHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }
        public async Task<GetFoodDrinkQuantityVm> Handle(GetFoodDrinkQuantity request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            var foodCount = await _context.FoodDrinkMenus
                          .Where(x => x.Type.Equals(Domain.Enums.type.Food))
                          .AsNoTracking()
                          .CountAsync(cancellationToken);

            var drinkCount = await _context.FoodDrinkMenus
                .Where(x => x.Type.Equals(Domain.Enums.type.Drink))
                .AsNoTracking()
                .CountAsync(cancellationToken);

            // var foodCount = await _context.FoodDrinkMenus
            //     .Where(x => (int)x.Type == (int)type.Food)
            //     .AsNoTracking()
            //     .CountAsync(cancellationToken);

            // var drinkCount = await _context.FoodDrinkMenus
            //     .Where(x => (int)x.Type == (int)type.Drink)
            //     .AsNoTracking()
            //     .CountAsync(cancellationToken);

            return new GetFoodDrinkQuantityVm
            {
                Status = "Ok",
                Data = new FoodDrinkQuantity
                {
                    FoodQuantity = foodCount,
                    DrinkQuantity = drinkCount,
                }
            };
        }
    }
}