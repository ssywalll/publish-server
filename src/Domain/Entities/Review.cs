using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Review : BaseAuditableEntity
    {
        public Reaction Reaction { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int User_Id { get; set; }
        public int Food_Drink_Id { get; set; }
        public User? User { get; set; }
    }
}