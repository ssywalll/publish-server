using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetOrderWithPagination : IRequest<PaginatedList<OrderWaitingDto>>
    {
        public string? SortBy { get; init; } = "Pesanan Terbaru";
        public int Filter { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetOrderWithPaginationHandler : IRequestHandler<GetOrderWithPagination, PaginatedList<OrderWaitingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OrderWaitingDto>> Handle(GetOrderWithPagination request, CancellationToken cancellationToken)
        {
            IQueryable<Order> orderData = _context.Orders;

            switch (request.Filter)
            {
                case 0:
                    orderData = orderData.Where(x => x.Status == 0);
                    break;
                case 1:
                    orderData = orderData.Where(x => x.Status == Domain.Enums.Status.OnProcces);
                    break;
                case 2:
                    orderData = orderData.Where(x => x.Status == Domain.Enums.Status.OnDelivery);
                    break;
                case 3:
                    orderData = orderData.Where(x => x.Status == Domain.Enums.Status.Successful);
                    break;
            }

            switch (request.SortBy)
            {
                case "Semua":
                    orderData = orderData.OrderBy(x => x.Id);
                    break;
                case "Pesanan Terbanyak":
                    orderData = orderData.OrderByDescending(x => x.FoodDrinkOrders!.Sum(y => y.Quantity));
                    break;
                case "Pesanan Terdikit":
                    orderData = orderData.OrderBy(x => x.FoodDrinkOrders!.Sum(y => y.Quantity));
                    break;
                case "Pesanan Terbaru":
                    orderData = orderData.OrderByDescending(x => x.Order_Time);
                    break;
                case "Pesanan Terlama":
                    orderData = orderData.OrderBy(x => x.Order_Time);
                    break;
            }


            return await orderData
               .ProjectTo<OrderWaitingDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}