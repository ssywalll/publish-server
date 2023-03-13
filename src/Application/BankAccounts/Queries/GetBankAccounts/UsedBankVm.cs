using CleanArchitecture.Application.Carts.Queries.GetCarts;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public record UsedBankVm
    {
        public string Status { get; init; } = string.Empty;
        public CheckoutBankDto? Data { get; init; }
    }
}