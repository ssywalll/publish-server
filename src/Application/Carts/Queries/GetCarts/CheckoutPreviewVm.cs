using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public record CheckoutBankDto : IMapFrom<BankAccount>
    {
        public string BankNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
    public record CheckoutPreviewDto
    {
        public float TotalPrice { get; init; }
        public CheckoutBankDto? UsedBank { get; init; }
        public IList<CheckoutCartDto>? Carts { get; init; }
    }
    public record CheckoutPreviewVm
    {
        public string? Status { get; init; }
        public CheckoutPreviewDto? Data { get; init; }
    }
}