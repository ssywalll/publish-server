using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.ExportOrder
{
    public record GetOrderAdmin : IRequest<GetByTokenVm>
    {
        public int Id { get; init; }
    }
    public class GetOrderAdminHandler : IRequestHandler<GetOrderAdmin, GetByTokenVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderAdminHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetByTokenVm> Handle(GetOrderAdmin request, CancellationToken cancellationToken)
        {

                var data = await _context.Orders
                    .Where(x => x.User_Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<GetByTokenDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var dataOrderUser = await _context.Orders
                    .Where(x => x.User_Id == request.Id)
                    .ToListAsync(cancellationToken);

                var totalPriceOrdered = await _context.Orders
                    .Where(x => x.User_Id == request.Id)
                    .SumAsync(y => y.FoodDrinkOrders!.Sum(z => z.FoodDrinkMenus!.Price * z.Quantity));


                return new GetByTokenVm
                {
                    Status = "Ok",
                    Data = new GetTotalOrder
                    {
                        TotalOrdered = dataOrderUser.Count(),
                        TotalPriceOrdered = totalPriceOrdered,
                        ListData = data
                    }
                };
        }
    }
}