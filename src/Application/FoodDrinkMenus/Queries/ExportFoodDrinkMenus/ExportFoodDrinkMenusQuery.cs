
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.ExportFoodDrinkMenus;

    public class ExportFoodDrinkMenusQuery : IRequest<FoodDrinkMenusVm>
    {
        public int Id { get; set; }
    }

    public class ExportFoodDrinkMenusQueryHandler : IRequestHandler<ExportFoodDrinkMenusQuery, FoodDrinkMenusVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportFoodDrinkMenusQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkMenusVm> Handle(ExportFoodDrinkMenusQuery request, CancellationToken cancellationToken)
        {
             return new FoodDrinkMenusVm
            {
                Status = "Ok",
                Data = await _context.FoodDrinkMenus
                    .Where(x => x.Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
}