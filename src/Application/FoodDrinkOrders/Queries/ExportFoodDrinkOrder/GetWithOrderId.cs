using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.FoodDrinkOrders.Queries.GetFoodDrinkOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FoodDrinkOrders.Queries.ExportFoodDrinkOrder
{
    public record GetWithOrderId : IRequest<FoodDrinkOrdersVm>
    {
        public int OrderId { get; init; }
    } 

    public class GetWithOrderIdHandler : IRequestHandler<GetWithOrderId, FoodDrinkOrdersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWithOrderIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FoodDrinkOrdersVm> Handle(GetWithOrderId request, CancellationToken cancellationToken)
        {
             return new FoodDrinkOrdersVm
            {
                Status = "Ok",
                Data = await _context.FoodDrinkOrders
                    .Where(x => x.Order_Id == request.OrderId)
                    .AsNoTracking()
                    .ProjectTo<FoodDrinkOrderDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}