using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class BankAccount : BaseAuditableEntity
    {
        public string Bank_Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Bank_Name { get; set; } = string.Empty;
        public int User_Id { get; set; }
    }
}