using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;
using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public record GetOrderWithToken : UseAprizax, IRequest<OrderWithTokenVm>
    {
        public int OrderId { get; init; }
    }

    public class GetOrderWithTokenHandler : IRequestHandler<GetOrderWithToken, OrderWithTokenVm>
    {
        private readonly IApplicationDbContext _context;
        private IMapper _mapper;
        public GetOrderWithTokenHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OrderWithTokenVm> Handle(GetOrderWithToken request, CancellationToken cancellationToken)
        {

            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var orderOwnerId = await _context.Orders
                .Where(x => x.Id.Equals(request.OrderId))
                .AsNoTracking()
                .Select(y => y.User_Id)
                .SingleOrDefaultAsync(cancellationToken);

            var isNotValidOwner = orderOwnerId.Equals(tokenInfo.Owner_Id) is false;

            if (isNotValidOwner)
                throw new NotFoundException("Order ini bukan milik anda", HttpStatusCode.BadRequest);

            var order = await _context.Orders
                .Where(x => x.Id.Equals(request.OrderId))
                .AsNoTracking()
                .ProjectTo<OrderWithTokenDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return new OrderWithTokenVm
            {
                Status = "Ok",
                Data = order
            };
        }
    }
}