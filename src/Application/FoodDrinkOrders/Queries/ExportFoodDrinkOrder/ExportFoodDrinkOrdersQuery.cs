using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.FoodDrinkOrders.Queries.GetFoodDrinkOrders;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkOrders.Queries.ExportFoodDrinkOrders;

    public record ExportFoodDrinkOrdersQuery: IRequest<FoodDrinkOrdersVm>
    {
        public int Id { get; set; }
    }

    public class ExportFoodDrinkOrdersQueryHandler : IRequestHandler<ExportFoodDrinkOrdersQuery, FoodDrinkOrdersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportFoodDrinkOrdersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkOrdersVm> Handle(ExportFoodDrinkOrdersQuery request, CancellationToken cancellationToken)
        {
             return new FoodDrinkOrdersVm
            {
                Data = await _context.FoodDrinkOrders
                    .Where(x => x.Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkOrderDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }

    }

