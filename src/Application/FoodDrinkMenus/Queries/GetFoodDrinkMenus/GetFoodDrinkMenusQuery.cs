using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    // [Authorize]
    public record GetFoodDrinkMenusQuery : IRequest<FoodDrinkMenusVm>;

    public class GetFoodDrinkMenusQueryHandler : IRequestHandler<GetFoodDrinkMenusQuery, FoodDrinkMenusVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodDrinkMenusQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenusVm> Handle(GetFoodDrinkMenusQuery request, CancellationToken cancellationToken)
        {
            return new FoodDrinkMenusVm
            {
                // Response = await _context.Responses
                //     .ProjectTo<ResponseDto>(_mapper.ConfigurationProvider)
                //     .ToListAsync(),

                Data = await _context.FoodDrinkMenus
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}