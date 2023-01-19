using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record GetFoodDrinkMenuFilter : IRequest<FoodDrinkMenusVm>
    {
        public string Name { get; init; } = string.Empty;
    }

    public class GetFoodDrinkMenuFilterHandler : IRequestHandler<GetFoodDrinkMenuFilter, FoodDrinkMenusVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodDrinkMenuFilterHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenusVm> Handle(GetFoodDrinkMenuFilter request, CancellationToken cancellationToken)
        {
            return new FoodDrinkMenusVm
            {
                Status = "Ok",
                Data = await _context.FoodDrinkMenus
                    // .Include(x => x.Name == request.Name)
                    // .Where(x => x.Name == request.Name)
                    .Where(x => x.Name.ToLower().Contains(request.Name))
                    .OrderBy(t => t.Price)
                    .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}