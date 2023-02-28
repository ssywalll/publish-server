using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using AutoMapper;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public class BankAccountDto : IMapFrom<BankAccount>
    {
        public int Id { get; set; }
        public string BankNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
}