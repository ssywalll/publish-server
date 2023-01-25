using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record GetFoodDrinkMenuWithPagination : IRequest<PaginatedList<FoodDrinkMenuDto>>
    {
        // public string Name { get; init; } = string.Empty;
        public string? SortBy { get; init; }
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
            IQueryable<FoodDrinkMenu> foodDrinkTable = _context.FoodDrinkMenus;
            switch (request.SortBy)
            {
                case "Name":
                    foodDrinkTable = foodDrinkTable.OrderBy(item => item.Name);
                    break;
                case "Price":
                    foodDrinkTable = foodDrinkTable.OrderBy(item => item.Price);
                    break;
                case "Date":
                    foodDrinkTable = foodDrinkTable.OrderBy(item => item.Created);
                    break;
            }
            return await foodDrinkTable
               .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}