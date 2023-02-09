using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand : IRequest
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public DateTime Meal_Date { get; set; }
        public string Address { get; set; } = string.Empty;
        public int BankAccount_Id { get; set; }
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
            entity.User_Id = request.User_Id;
            entity.Meal_Date = request.Meal_Date;
            entity.Address = request.Address;
            entity.BankAccount_Id = request.BankAccount_Id;




            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}