using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;

namespace CleanArchitecture.Application.Carts.Commands.CreateCart
{
    public record CreateCartCommand : IRequest<int>
    {
        public int User_Id { get; init; }
        public int Food_Drink_Id { get; init; }
        public int Quantity { get; init; }
    }
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand ,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCartCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
           var entity = new Cart();

           entity.User_Id = request.User_Id; 

           _context.Carts.Add(entity);

           await _context.SaveChangesAsync(cancellationToken);

           return entity.User_Id;
        }
    }
}