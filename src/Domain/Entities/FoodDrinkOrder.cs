using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class FoodDrinkOrder : BaseAuditableEntity
    {
        public int Food_Drink_Id { get; set; }
        public int Order_Number { get; set; }
    }
}