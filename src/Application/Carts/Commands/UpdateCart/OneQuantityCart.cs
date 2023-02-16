using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;

namespace CleanArchitecture.Application.Carts.Commands.UpdateCart
{
    public class OneQuantityCartHandler : IRequestHandler<OneQuantityCartDto, OneQuantityCartVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OneQuantityCartHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OneQuantityCartVm> Handle(OneQuantityCartDto request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null!;

            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                return new OneQuantityCartVm()
                {
                    Status = "Error",
                    LatestQuantity = null
                };

            var entity = await _context.Carts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null) return new OneQuantityCartVm()
            {
                Status = "Error",
                LatestQuantity = null
            };

            if (request.Is_Increament)
                entity.Quantity++;
            else
                entity.Quantity--;

            await _context.SaveChangesAsync(cancellationToken);

            return new OneQuantityCartVm()
            {
                Status = "Error",
                LatestQuantity = null
            };
        }


    }
}