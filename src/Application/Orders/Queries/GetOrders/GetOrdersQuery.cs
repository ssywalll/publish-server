using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery : IRequest<OrdersVm>;

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrdersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrdersVm> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return new OrdersVm
            {
                Status = "Ok",
                Data = await _context.Orders
                    .AsNoTracking()
                    .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}