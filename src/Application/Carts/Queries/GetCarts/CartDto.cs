using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public FoodDrinkMenu? FoodDrinkMenu { get; set; }
        public CartDto()
        {
            Food_Drink_Menu_Name = FoodDrinkMenu?.Name;
            Food_Drink_Menu_Price = FoodDrinkMenu?.Price;
            Food_Drink_Menu_Min_Order = FoodDrinkMenu?.Min_Order;
        }
    }
}