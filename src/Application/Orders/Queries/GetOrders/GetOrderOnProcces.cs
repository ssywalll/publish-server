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
    public record GetOrderOnProcces : IRequest<PaginatedList<OrderWaitingDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    } 
    
    public class GetOrderOnProccesHandler : IRequestHandler<GetOrderOnProcces, PaginatedList<OrderWaitingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderOnProccesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper =  mapper;
        }

        public async Task<PaginatedList<OrderWaitingDto>> Handle(GetOrderOnProcces request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                    .Where(x => x.Status == Domain.Enums.Status.OnProcces)
                    .ProjectTo<OrderWaitingDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}