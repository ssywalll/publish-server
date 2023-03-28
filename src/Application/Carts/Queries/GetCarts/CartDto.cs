using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public class CartDto : IMapFrom<Cart>
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public bool IsChecked { get; set; }
        public int Quantity { get; set; }
        public string? Food_Drink_Menu_Name { get; set; }
        public float? Food_Drink_Menu_Price { get; set; }
        public int? Food_Drink_Menu_Min_Order { get; set; }
        public string? ImageUrl { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cart, CartDto>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => AprizaxImages.GetBinaryImage(s.FoodDrinkMenu!.Image_Url)))
                .ForMember(d => d.Food_Drink_Menu_Name, opt => opt.MapFrom(s => s.FoodDrinkMenu!.Name))
                .ForMember(d => d.Food_Drink_Menu_Price, opt => opt.MapFrom(s => s.FoodDrinkMenu!.Price))
                .ForMember(d => d.Food_Drink_Menu_Min_Order, opt => opt.MapFrom(s => s.FoodDrinkMenu!.Min_Order));
        }
    }
}