using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Carts.Queries.GetCarts;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Carts.Queries.ExportCarts;

public class ExportCartsQuery : IRequest<CartsVm>
{
    public int Id { get; set; }
}

public class ExportCartsQueryHandler : IRequestHandler<ExportCartsQuery, CartsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExportCartsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartsVm> Handle(ExportCartsQuery request, CancellationToken cancellationToken)
    {
        return new CartsVm
        {
            Data = await _context.Carts
               .Where(x => x.Id == request.Id)
               .AsNoTracking()
               .ProjectTo<CartDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken)
        };
    }

}

