using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetOrderUser : UseAprizax, IRequest<PaginatedList<OrderWaitingDto>>
    {
        public int Filter { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetOrderUserHandler : IRequestHandler<GetOrderUser, PaginatedList<OrderWaitingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrderUserHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OrderWaitingDto>> Handle(GetOrderUser request, CancellationToken cancellationToken)
        {
            IQueryable<Order> orderData = _context.Orders;
            var ownerId = request.GetOwnerId();

            switch (request.Filter)
            {
                case 0:
                    orderData = orderData
                    .Where(w => w.User_Id == ownerId)
                    .Where(x => x.Status == 0);
                    break;
                case 1:
                    orderData = orderData
                    .Where(w => w.User_Id == ownerId)
                    .Where(x => x.Status == Domain.Enums.Status.OnProcces);
                    break;
                case 2:
                    orderData = orderData
                    .Where(w => w.User_Id == ownerId)
                    .Where(x => x.Status == Domain.Enums.Status.OnDelivery);
                    break;
                case 3:
                    orderData = orderData
                    .Where(w => w.User_Id == ownerId)
                    .Where(x => x.Status == Domain.Enums.Status.Successful);
                    break;
            }

            return await orderData
               .ProjectTo<OrderWaitingDto>(_mapper.ConfigurationProvider)
               .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}