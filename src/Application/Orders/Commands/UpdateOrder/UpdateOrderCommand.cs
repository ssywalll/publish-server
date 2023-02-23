using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System.Net;

namespace CleanArchitecture.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsForward { get; set; }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Orders), request.Id);
            }


            if (request.IsForward)
            {
                if (entity.Status == Domain.Enums.Status.Successful)
                    throw new NotFoundException("Status Max", HttpStatusCode.BadRequest);
                entity.Status++;
            }
            else
            {
                if (entity.Status == 0 )
                    throw new NotFoundException("Status Min", HttpStatusCode.BadRequest);
                entity.Status--;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}