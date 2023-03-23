using AutoMapper;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public class FoodDrinkMenuDto : IMapFrom<FoodDrinkMenu>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Min_Order { get; set; }
        public string Description { get; set; } = string.Empty;
        public type Type { get; set; }
        public int Like { get; set; }
        public int Ok { get; set; }
        public int Dislike { get; set; }
        public string Image_Url { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FoodDrinkMenu, FoodDrinkMenuDto>()
                .ForMember(d => d.Image_Url, opt => opt.MapFrom(s => AprizaxImages.GetBinaryImage(s.Image_Url)))
                .ForMember(d => d.Like, opt => opt.MapFrom(s => s.Reviews!.Where(x => x.Food_Drink_Id == s.Id).Where(y => y.Reaction == Domain.Enums.Reaction.Like).Count()))
                .ForMember(d => d.Ok, opt => opt.MapFrom(s => s.Reviews!.Where(x => x.Food_Drink_Id == s.Id).Where(y => y.Reaction == Domain.Enums.Reaction.Ok).Count()))
                .ForMember(d => d.Dislike, opt => opt.MapFrom(s => s.Reviews!.Where(x => x.Food_Drink_Id == s.Id).Where(y => y.Reaction == Domain.Enums.Reaction.Dislike).Count()));
        }
    }
}