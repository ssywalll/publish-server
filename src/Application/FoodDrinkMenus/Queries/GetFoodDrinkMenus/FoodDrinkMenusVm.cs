using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus
{
    public class FoodDrinkMenusVm
    {
        public IList<FoodDrinkMenuDto> Data { get; set; } = new List<FoodDrinkMenuDto>();
        // public IList<ResponseDto> Response { get; set; } = new List<ResponseDto>();
    }
}