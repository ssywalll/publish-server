namespace CleanArchitecture.Application.Carts.Commands.CreateCart;

public record CreateCartVm
{
    public string? Status { get; init; }
    public string? LatestQuantity { get; init; }
}