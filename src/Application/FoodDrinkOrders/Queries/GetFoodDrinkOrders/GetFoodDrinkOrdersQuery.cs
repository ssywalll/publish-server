using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkOrders.Queries.GetFoodDrinkOrders
{
    public record GetFoodDrinkOrdersQuery : IRequest<FoodDrinkOrdersVm>;

    public class GetFoodDrinkOrdersQueryHandler : IRequestHandler<GetFoodDrinkOrdersQuery, FoodDrinkOrdersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodDrinkOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkOrdersVm> Handle(GetFoodDrinkOrdersQuery request, CancellationToken cancellationToken)
        {
            return new FoodDrinkOrdersVm
            {
                Data = await _context.FoodDrinkOrders
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkOrderDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}