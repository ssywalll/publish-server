namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public class BankAccountsVm
    {
        public string Status { get; init; } = string.Empty;
        public bool IsLimit { get; init; }
        public IList<BankAccountDto> Data { get; init; } = new List<BankAccountDto>();
    }
}