using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public class FoodReactionVm
    {
        public string Status { get; set; } = string.Empty;
        public FoodDrinkMenuDto? Data { get; set; } 
    }
}