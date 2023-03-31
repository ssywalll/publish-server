using MediatR;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Net;

namespace CleanArchitecture.Application.Orders.Commands.UpdateOrder
{
    public record OrderStatusFullChange : IRequest
    {
        public int Id { get; init; }
        public Status Status { get; init; }
    }

    public class OrderStatusFullChangeHandler : IRequestHandler<OrderStatusFullChange>
    {
        private readonly IApplicationDbContext _context;
        private IMapper _mapper;
        public OrderStatusFullChangeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(OrderStatusFullChange request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            if (Enum.IsDefined<Status>(request.Status) is false)
                throw new NotFoundException($"Order status {request.Status} tidak ada!", HttpStatusCode.BadRequest);

            var entity = await _context.Orders
                .Where(x => x.Id.Equals(request.Id))
                .SingleOrDefaultAsync(cancellationToken);

            if (entity is null)
                throw new NotFoundException("Order yang anda maksud tidak ada!", HttpStatusCode.BadRequest);

            if (entity.Status.Equals(request.Status))
                throw new NotFoundException("Anda tidak merubah status order!", HttpStatusCode.BadRequest);

            entity.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}