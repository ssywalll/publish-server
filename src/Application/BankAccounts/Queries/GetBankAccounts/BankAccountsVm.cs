using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public class BankAccountsVm
    {
        public IList<BankAccountDto> BankAccountDtos { get; set; } = new List<BankAccountDto>();
    }
}