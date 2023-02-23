using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery : IRequest<OrderDto>
    {
        public int Id { get; init;}
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
             return await _context.Orders
                    .Where(x => x.Id.Equals(request.Id))
                    .AsNoTracking()
                    .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
        }
    }
}