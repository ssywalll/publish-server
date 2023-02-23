using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record GetFoodDrinkMenusQuery : IRequest<FoodDrinkMenuDto>;

    public class GetFoodDrinkMenusQueryHandler : IRequestHandler<GetFoodDrinkMenusQuery, FoodDrinkMenuDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodDrinkMenusQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenuDto> Handle(GetFoodDrinkMenusQuery request, CancellationToken cancellationToken)
        {
            return await _context.FoodDrinkMenus
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        }
    }
}