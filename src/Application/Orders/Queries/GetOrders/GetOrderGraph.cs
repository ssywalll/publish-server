using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
     public record GetOrderGraph : IRequest<PaginatedList<ItemGraphDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetOrderGraphHandler : IRequestHandler<GetOrderGraph, PaginatedList<ItemGraphDto>>
    {
         private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderGraphHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ItemGraphDto>> Handle(GetOrderGraph request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Where(x => x.Status == Domain.Enums.Status.Successful)
                .OrderByDescending(x => x.Id )
                .ProjectTo<ItemGraphDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}