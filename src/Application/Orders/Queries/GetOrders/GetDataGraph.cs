using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetDataGraph : IRequest<DataGraphVm>
    {
        public int? Filter { get; init; }
    }

    public class GetDataGraphHandler : IRequestHandler<GetDataGraph, DataGraphVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetDataGraphHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataGraphVm> Handle(GetDataGraph request, CancellationToken cancellationToken)
        {
            IQueryable<Order> orderData = _context.Orders;

            var entity = await _context.FoodDrinkOrders
                .Where(x => x.Orders!.Status.Equals(Domain.Enums.Status.Successful))
                .SumAsync(x => x.Quantity * x.FoodDrinkMenus!.Price);

            var order = await _context.FoodDrinkOrders
                .Where(x => x.Orders!.Status.Equals(Domain.Enums.Status.Successful))
                .SumAsync(x => x.Quantity);

            var dataGraph = await _context.Orders
                .Where(x => x.Status.Equals(Domain.Enums.Status.Successful))
                .AsNoTracking()
                .ProjectTo<DataGraphDto2>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            switch (request.Filter)
            {
                case 1:
                    orderData = orderData.Where(x => x.Order_Time.Month == 01);
                    break;
                case 2:
                    orderData = orderData.Where(x => x.Order_Time.Month == 02);
                    break;
                case 3:
                    orderData = orderData.Where(x => x.Order_Time.Month == 03);
                    break;
                case 4:
                    orderData = orderData.Where(x => x.Order_Time.Month == 04);
                    break;
                case 5:
                    orderData = orderData.Where(x => x.Order_Time.Month == 05);
                    break;
                case 6:
                    orderData = orderData.Where(x => x.Order_Time.Month == 06);
                    break;
                case 7:
                    orderData = orderData.Where(x => x.Order_Time.Month == 07);
                    break;
                case 8:
                    orderData = orderData.Where(x => x.Order_Time.Month == 08);
                    break;
                case 9:
                    orderData = orderData.Where(x => x.Order_Time.Month == 09);
                    break;
                case 10:
                    orderData = orderData.Where(x => x.Order_Time.Month == 10);
                    break;
                case 11:
                    orderData = orderData.Where(x => x.Order_Time.Month == 11);
                    break;
                case 12:
                    orderData = orderData.Where(x => x.Order_Time.Month == 12);
                    break;
            }

            var dataOrderTime = request.Filter == null ?
                await _context.Orders
                    .AsNoTracking()
                    .OrderBy(w => w.Id)
                    .ProjectTo<DataGraphDto>(_mapper.ConfigurationProvider)
                    .Select(x => x.OrderTime)
                    .ToListAsync(cancellationToken)
                :
                await orderData
                    .AsNoTracking()
                    .OrderBy(w => w.Id)
                    .ProjectTo<DataGraphDto>(_mapper.ConfigurationProvider)
                    .Select(x => x.OrderTime)
                    .ToListAsync(cancellationToken);

            var dataTotalOrder = request.Filter == null ?
                await _context.Orders
                    .AsNoTracking()
                    .GroupBy(y => y.Order_Time.Date)
                    .OrderBy(y => y.Key)
                    .Select(z => z.Sum(a => a.FoodDrinkOrders!.Sum(t => t.Quantity)))
                    .ToListAsync(cancellationToken)
                :
                await orderData
                    .AsNoTracking()
                    .GroupBy(y => y.Order_Time.Date)
                    .OrderBy(y => y.Key)
                    .Select(z => z.Sum(a => a.FoodDrinkOrders!.Sum(t => t.Quantity)))
                    .ToListAsync(cancellationToken);

            var data1 = dataTotalOrder.Any() ? dataTotalOrder.Last() : 0;
            var data2 = dataTotalOrder.Count() >= 2 ? dataTotalOrder[^2] : 0;
            var dataComparison = Aprizax.GetDataComparison(data1, data2, order);

            return new DataGraphVm
            {
                Status = "Ok",
                Data = new GraphVm
                {
                    TotalIncome = entity,
                    TotalOrdered = order,
                    DataComparison = dataComparison,
                    DataTotalOrder = dataTotalOrder,
                    DataOrderTime = dataOrderTime.Distinct().ToList(),
                    DataGraph = dataGraph
                }
            };
        }
    }
}