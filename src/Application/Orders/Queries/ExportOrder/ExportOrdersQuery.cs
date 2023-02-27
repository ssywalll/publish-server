using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Orders.Queries.GetOrders;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.ExportOrders
{
    public record ExportOrdersQuery : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }

    public class ExportOrdersQueryHandler : IRequestHandler<ExportOrdersQuery, OrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(ExportOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
               .Where(x => x.Id == request.Id)
               .AsNoTracking()
               .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
               .SingleAsync(cancellationToken);
        }

    }

}


