using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.FoodDrinkOrders.Queries.GetFoodDrinkOrders
{
    public class FoodDrinkOrderDto : IMapFrom<FoodDrinkOrder>
    {
        public int Id { get; set; }
        public int Food_Drink_Id { get; set; }
        public string? FoodDrinkName { get; set; }
        public int Order_Id { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string? imagePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FoodDrinkOrder, FoodDrinkOrderDto>()
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.FoodDrinkMenus!.Price))
                .ForMember(d => d.imagePath, opt => opt.MapFrom(s => AprizaxImages.GetBinaryImage(s.FoodDrinkMenus!.Image_Url)))
                .ForMember(d => d.FoodDrinkName, opt => opt.MapFrom(s => s.FoodDrinkMenus!.Name));
        }
    }
}