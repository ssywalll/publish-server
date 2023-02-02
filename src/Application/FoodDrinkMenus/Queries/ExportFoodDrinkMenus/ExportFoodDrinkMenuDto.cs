using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.ExportFoodDrinkMenus
{
    public class ExportFoodDrinkMenuDto : IMapFrom<Review>
    {
        public Reaction Reaction { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string? User_Name { get; set; }
        public int Food_Drink_Id { get; set; }
    }
}