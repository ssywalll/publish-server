using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class BankAccount : BaseAuditableEntity
    {
        public int Number { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Bank_User { get; set; } = string.Empty;
        public int User_Id { get; set; }

        // public User User { get; set; }
    }
}