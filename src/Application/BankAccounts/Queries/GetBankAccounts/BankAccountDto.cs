using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public class BankAccountDto : IMapFrom<BankAccount>
    {
        public int Id { get; set; }
        public string Bank_Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Bank_User { get; set; } = string.Empty;
        public int User_Id { get; set; }
    }
}