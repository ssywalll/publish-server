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

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetDataGraph : IRequest<DataGraphVm>;

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
            var entity = await _context.FoodDrinkOrders
                .Where(x => x.Orders!.Status.Equals(Domain.Enums.Status.Successful))
                .SumAsync(x => x.Quantity * x.FoodDrinkMenus!.Price);

            var order = await _context.FoodDrinkOrders
                .Where(x => x.Orders!.Status.Equals(Domain.Enums.Status.Successful))
                .SumAsync(x => x.Quantity);

            var dataGraph = await _context.Orders
                .AsNoTracking()
                .ProjectTo<DataGraphDto2>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var dataOrderTime = await _context.Orders
                .AsNoTracking()
                .OrderBy(w => w.Id)
                .ProjectTo<DataGraphDto>(_mapper.ConfigurationProvider)
                .Select(x => x.OrderTime)
                .ToListAsync(cancellationToken);

            var dataTotalOrder = await _context.Orders
                .AsNoTracking()
                .GroupBy(y => y.Order_Time.Date)
                .OrderBy(y => y.Key)
                .Select(z => z.Sum(a => a.FoodDrinkOrders!.Sum(t => t.Quantity)))
                .ToListAsync(cancellationToken);

            // var dataComparison = Aprizax.GetDataComparison(dataTotalOrder.Last(), dataTotalOrder.Last() -1, order);
            var dataComparison = Aprizax.GetDataComparison(dataTotalOrder.Last(), dataTotalOrder.LastIndexOf(dataTotalOrder.Last() -1), order);
            // var dataComparison = Aprizax.GetDataComparison(57,5, order);

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