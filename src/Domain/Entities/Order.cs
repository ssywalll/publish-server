using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public int Number { get; set; }
        public int User_Id { get; set; }
        public DateTime Order_Time { get; set; }
        public DateTime Meal_Date { get; set; }
        public Status Status { get; set; }
        public int Bank_Number { get; set; }
        public string Payment_Url { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}