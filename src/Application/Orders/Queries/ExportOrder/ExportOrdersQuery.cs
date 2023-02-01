using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Orders.Queries.GetOrders;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.ExportOrders
{
    public record ExportOrdersQuery : IRequest<OrdersVm>
    {
        public int Id { get; set; }
    }

    public class ExportOrdersQueryHandler : IRequestHandler<ExportOrdersQuery, OrdersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrdersVm> Handle(ExportOrdersQuery request, CancellationToken cancellationToken)
        {
            return new OrdersVm
            {
                Data = await _context.Orders
                   .Where(x => x.Id == request.Id)
                   .AsNoTracking()
                   .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken)
            };
        }

    }

}


