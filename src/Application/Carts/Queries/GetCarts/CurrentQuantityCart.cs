using AutoMapper;
using MediatR;
using System.Net;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Carts.Commands.DeleteCart
{
    public record CurrentQuantityCartVm
    {
        public string? Status { get; set; }
        public string? Data { get; set; }
    }
    public record CurrentQuantityCart : UseAprizax, IRequest<CurrentQuantityCartVm> { }

    public class CurrentQuantityCartHandler : IRequestHandler<CurrentQuantityCart, CurrentQuantityCartVm>
    {
        private readonly IApplicationDbContext _context;

        public CurrentQuantityCartHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
        }
        public async Task<CurrentQuantityCartVm> Handle(CurrentQuantityCart request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            return new CurrentQuantityCartVm
            {
                Status = "Ok",
                Data = _context.Carts.GetCurrentQuantity(tokenInfo.Owner_Id)
            };
        }
    }
}