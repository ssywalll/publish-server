using AutoMapper;
using MediatR;
using System.Net;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Carts.Commands.UpdateCart
{
    public record CheckboxTriggerCart : UseAprizax, IRequest
    {
        public int Cart_Id { get; set; }
    }

    public class CheckboxTriggerCartHandler : IRequestHandler<CheckboxTriggerCart>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CheckboxTriggerCartHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CheckboxTriggerCart request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);
            var cartTarget = _context.Carts.First(x => x.Id.Equals(request.Cart_Id));
            if (cartTarget.User_Id.Equals(tokenInfo.Owner_Id) is false)
            {
                var action = cartTarget.IsChecked ? "check" : "uncheck";
                throw new NotFoundException($"Barang yang anda {action} tidak ada di keranjang anda!", HttpStatusCode.BadRequest);
            }

            cartTarget.IsChecked = !cartTarget.IsChecked;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}