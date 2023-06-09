using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.FoodDrinkOrders.Queries.GetFoodDrinkOrders
{
    public class FoodDrinkOrdersVm
    {
        public string Status { get; set; } = string.Empty;
         public IList<FoodDrinkOrderDto> Data { get; set; } = new List<FoodDrinkOrderDto>();
    }
}