using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record GetByIdFoodDrinkMenusQuery(int Id) : IRequest<FoodDrinkMenusVm>;
    
    public class GetByIdFoodDrinkMenusQueryHandler : IRequestHandler<GetByIdFoodDrinkMenusQuery, FoodDrinkMenusVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper; 

        public GetByIdFoodDrinkMenusQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenusVm> Handle(GetByIdFoodDrinkMenusQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.FoodDrinkMenus
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
            
            if(entity == null)
            {
                throw new NotFoundException(nameof(FoodDrinkMenusVm), request.Id);
            }

            return new FoodDrinkMenusVm
            {
                Data = await _context.FoodDrinkMenus
                    .Where(l => l.Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Name)
                    .ToListAsync(cancellationToken)
            };
        }
    }

   
}