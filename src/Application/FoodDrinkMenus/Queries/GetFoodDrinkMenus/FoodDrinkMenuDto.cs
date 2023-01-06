using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public class FoodDrinkMenuDto : IMapFrom<FoodDrinkMenu>
    {
        public int Id { get ; set; }
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Min_Order { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image_Url { get; set; } = string.Empty;
    }
}