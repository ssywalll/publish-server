using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Commands.CreateCart
{
    public record CreateCartCommand : UseAprizax, IRequest<CreateCartVm>
    {
        public int Food_Drink_Id { get; init; }
    }
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CreateCartVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCartCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateCartVm> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return null!;

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false) return null!;

            var cartTarget = await _context.Carts.FirstOrDefaultAsync(
                x => (x.User_Id == tokenInfo.Owner_Id) && (x.Food_Drink_Id == request.Food_Drink_Id)
            );

            var foodDrinkTarget = await _context.FoodDrinkMenus
               .FirstAsync(x => x.Id.Equals(request.Food_Drink_Id));

            if (cartTarget is null)
            {
                var entity = new Cart
                {
                    User_Id = tokenInfo.Owner_Id ?? 0,
                    Food_Drink_Id = request.Food_Drink_Id,
                    Quantity = foodDrinkTarget.Min_Order
                };
                _context.Carts.Add(entity);
            }
            else
            {
                cartTarget.Quantity++;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateCartVm
            {
                Status = "Ok",
                CurrentQuantity = _context.Carts.GetCurrentQuantity(tokenInfo.Owner_Id)
            };
        }
    }
}