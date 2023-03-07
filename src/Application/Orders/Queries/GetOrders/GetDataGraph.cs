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
                .SumAsync(x => x.Quantity * x.FoodDrinkMenus!.Price);

            var order = await _context.FoodDrinkOrders
                .Where(x => x.Orders!.Status.Equals(Domain.Enums.Status.Successful))
                .SumAsync(x => x.Quantity);

            var dataGraph = await _context.Orders
                .AsNoTracking()
                .ProjectTo<DataGraphDto2>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var dataIncomeOrder = await _context.Orders
                .AsNoTracking()
                .ProjectTo<DataGraphDto>(_mapper.ConfigurationProvider)
                .Select(x => x.TotalOrder)
                .ToListAsync(cancellationToken);

            var dataNow = await _context.Orders
                .Where(x => x.Order_Time.Date == DateTime.Now.Date)
                .AsNoTracking()
                .ProjectTo<DataGraphDto>(_mapper.ConfigurationProvider)
                .Select(x => x.TotalOrder)
                .ToListAsync(cancellationToken);

            List<int> AuthorList = new List<int>();

            var total = dataNow.Sum(x => Convert.ToInt32(x));
            AuthorList.Add(total);


            List<int> reverse = Enumerable.Reverse(AuthorList).ToList();

            var dataOrderTime = await _context.Orders
                .AsNoTracking()
                .OrderBy(w => w.Id)
                .ProjectTo<DataGraphDto>(_mapper.ConfigurationProvider)
                .Select(x => x.OrderTime)
                .ToListAsync(cancellationToken);

            var dataComparison = Aprizax.GetDataComparison(dataIncomeOrder.Last(), dataIncomeOrder.Last() - 1, order);

            return new DataGraphVm
            {
                Status = "Ok",
                Data = new GraphVm
                {
                    TotalIncome = entity,
                    TotalOrdered = order,
                    DataComparison = dataComparison,
                    DataTotalOrder = reverse,
                    DataOrderTime = dataOrderTime.Distinct().ToList(),
                    DataGraph = dataGraph
                }
            };
        }
    }
}