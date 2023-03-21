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
        public string Image_Url { get; set; } = string.Empty;
        public type Type { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FoodDrinkMenu, FoodDrinkMenuDto>()
            .ForMember(d => d.Image_Url, opt => opt.MapFrom(s => AprizaxImages.GetBinaryImage(s.Image_Url)));
        }

    }
}