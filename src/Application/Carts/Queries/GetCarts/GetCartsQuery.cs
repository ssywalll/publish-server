using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public record GetCartsQuery : IRequest<CartsVm>;

    public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, CartsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCartsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartsVm> Handle(GetCartsQuery request, CancellationToken cancellationToken)
        {
            return new CartsVm
            {
                Data = await _context.Carts
                    .AsNoTracking()
                    .ProjectTo<CartDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}