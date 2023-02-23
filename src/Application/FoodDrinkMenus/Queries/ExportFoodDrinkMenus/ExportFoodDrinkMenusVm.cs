using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.ExportFoodDrinkMenus
{
    public class ExportFoodDrinkMenusVm
    {
        public string Status { get; set; } = string.Empty;
        public IList<FoodDrinkMenuDto> Data { get; set; } = new List<FoodDrinkMenuDto>();
    }
}