using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.CreateOrders.Commands.CreateOrder
{
    public record CreateOrderCommand : IRequest<Order>
    {
        public int User_Id { get; set; }
        public string Meal_Date { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int BankAccount_Id { get; set; }

    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IApplicationDbContext _context;

        public CreateOrderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = new Order
            {
                Meal_Date = request.Meal_Date,
                Address = request.Address,
                User_Id = request.User_Id,
                BankAccount_Id = request.BankAccount_Id
            };

            _context.Orders.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}