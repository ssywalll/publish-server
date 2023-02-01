using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Tag : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Food_Drink_Id { get; set; }
        public FoodDrinkMenu? FoodDrinkMenus { get; set; }
    }
}