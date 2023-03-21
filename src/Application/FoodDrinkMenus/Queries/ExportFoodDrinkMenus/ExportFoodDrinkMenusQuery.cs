
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


public record ExportFoodDrinkMenusQuery : IRequest<FoodReactionVm>
{
    public int Id { get; init; }
}

public class ExportFoodDrinkMenusQueryHandler : IRequestHandler<ExportFoodDrinkMenusQuery, FoodReactionVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExportFoodDrinkMenusQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FoodReactionVm> Handle(ExportFoodDrinkMenusQuery request, CancellationToken cancellationToken)
    {
        var foodData = await _context.FoodDrinkMenus
            .Where(x => x.Id == request.Id)
            .AsNoTracking()
            .ProjectTo<FoodDrinkMenuDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        var likeCount = await _context.Reviews
            .Where(x => (
                x.Food_Drink_Id.Equals(request.Id) &&
                x.Reaction.Equals(Domain.Enums.Reaction.Like)
            ))
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var okCount = await _context.Reviews
            .Where(x => (
                x.Food_Drink_Id.Equals(request.Id) &&
                x.Reaction.Equals(Domain.Enums.Reaction.Ok)
            ))
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var dislikeCount = await _context.Reviews
            .Where(x => (
                x.Food_Drink_Id.Equals(request.Id) &&
                x.Reaction.Equals(Domain.Enums.Reaction.Dislike)
            ))
            .AsNoTracking()
            .CountAsync(cancellationToken);


        return new FoodReactionVm
        {
            Status = "Ok",
            Data = new FoodDrinkMenuDto
            {
                Id = foodData!.Id,
                Name = foodData.Name,
                Price = foodData.Price,
                Min_Order = foodData.Min_Order,
                Description = foodData.Description,
                Image_Url = foodData.Image_Url,
                Type = foodData.Type,
                Like = likeCount,
                Ok = okCount,
                Dislike = dislikeCount
            }
        };

    }
}