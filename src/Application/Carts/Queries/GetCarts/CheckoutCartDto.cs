using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public class CheckoutCartDto : IMapFrom<Cart>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? MenuName { get; set; }
        public float MenuPrice { get; set; }
        public float MenuTotalPrice { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cart, CheckoutCartDto>()
                .ForMember(d => d.MenuName, opt => opt.MapFrom(s => s.FoodDrinkMenu!.Name))
                .ForMember(d => d.MenuPrice, opt => opt.MapFrom(s => s.FoodDrinkMenu!.Price))
                .ForMember(d => d.MenuTotalPrice, opt => opt.MapFrom(s => s.Quantity * s.FoodDrinkMenu!.Price));
        }
    }
}