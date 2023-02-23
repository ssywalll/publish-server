
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.ExportFoodDrinkMenus;


public record ExportFoodDrinkMenusQuery : IRequest<FoodDrinkMenuDto>
{
    public int Id { get; init; }
}

public class ExportFoodDrinkMenusQueryHandler : IRequestHandler<ExportFoodDrinkMenusQuery, FoodDrinkMenuDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExportFoodDrinkMenusQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FoodDrinkMenuDto> Handle(ExportFoodDrinkMenusQuery request, CancellationToken cancellationToken)
    {
        return await _context.FoodDrinkMenus
                 .Where(x => x.Id == request.Id)
                 .AsNoTracking()
                 .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                 .SingleOrDefaultAsync(cancellationToken);
    }
}