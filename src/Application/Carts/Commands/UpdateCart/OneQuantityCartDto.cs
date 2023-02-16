
using MediatR;
using CleanArchitecture.Application.Common.Context;
namespace CleanArchitecture.Application.Carts.Commands.UpdateCart
{
    public record OneQuantityCartDto : UseAprizax, IRequest<OneQuantityCartVm>
    {
        public int Id { get; init; }
        public bool Is_Increament { get; init; }
    }
}