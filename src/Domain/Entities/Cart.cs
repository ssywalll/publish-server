using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Cart : BaseAuditableEntity
    {
        public int User_Id { get; set; }
        public int Food_Drink_Id { get; set; }
        public int Quantity { get; set; }
    }
}