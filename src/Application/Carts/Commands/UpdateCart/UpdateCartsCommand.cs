using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;


namespace CleanArchitecture.Application.Carts.Commands.UpdateCart
{
    public class UpdateCartsCommand : IRequest
    {
        public int Id { get; init; }
        public int User_Id { get; init; }
        public int Food_Drink_Id { get; init; }
        public int Quantity { get; init; }

    }

    public class UpdateCartsCommandHandler : IRequestHandler<UpdateCartsCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCartsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCartsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Carts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Carts), request.Id);
            }

            entity.Id = request.Id;
            entity.User_Id = request.User_Id;
            entity.Food_Drink_Id = request.Food_Drink_Id;
            entity.Quantity = request.Quantity;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }


    }
}