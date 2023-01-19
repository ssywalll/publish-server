using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record GetFoodDrinkMenuWithPagination : IRequest<PaginatedList<FoodDrinkMenuDto>>
    {
        // public string Name { get; init; } = string.Empty;
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
    
    public class GetFoodDrinkMenuWithPaginationHandler : IRequestHandler<GetFoodDrinkMenuWithPagination, PaginatedList<FoodDrinkMenuDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFoodDrinkMenuWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FoodDrinkMenuDto>> Handle(GetFoodDrinkMenuWithPagination request, CancellationToken cancellationToken)
        {
             return await _context.FoodDrinkMenus
                // .Where(x => x.Name == request.Name)
                .OrderBy(x => x.Name)
                .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}