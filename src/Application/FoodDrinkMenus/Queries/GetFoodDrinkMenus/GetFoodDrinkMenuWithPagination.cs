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
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public record GetFoodDrinkMenuWithPagination : IRequest<PaginatedList<FoodDrinkMenuDto>>
    {
        public string? Keyword { get; init; }
        public string? SortBy { get; init; }
        public int Filter { get; init; } = 2;
        public bool IsAsc { get; init; } = true;
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
            if (String.IsNullOrWhiteSpace(request.Keyword) is false)
                foodDrinkTable = foodDrinkTable.Where(
                    x => (x.Name.ToLower() + x.Description.ToLower())
                    .Contains(request.Keyword.ToLower())
                );

            switch (request.SortBy)
            {
                case "Name":
                    foodDrinkTable = foodDrinkTable.OrderBySwitch(x => x.Name, request.IsAsc);
                    break;
                case "Price":
                    foodDrinkTable = foodDrinkTable.OrderBySwitch(x => x.Price, request.IsAsc);
                    break;
                case "Date":
                    foodDrinkTable = foodDrinkTable.OrderBySwitch(x => x.Created, request.IsAsc);
                    break;
            }

            switch (request.Filter)
            {
                case 0:
                    foodDrinkTable = foodDrinkTable.Where(x => x.Type == 0);
                    break;
                case 1:
                    foodDrinkTable = foodDrinkTable.Where(x => x.Type == Domain.Enums.type.Drink);
                    break;
            }

            return await foodDrinkTable
               .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}