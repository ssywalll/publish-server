using AutoMapper;
using MediatR;
using System.Net;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Carts.Commands.UpdateCart
{
    public record CheckboxAllCart : UseAprizax, IRequest { }

    public class CheckboxAllCartHandler : IRequestHandler<CheckboxAllCart>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CheckboxAllCartHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CheckboxAllCart request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            var cartCollection = _context.Carts
                .Where(x => x.User_Id.Equals(tokenInfo.Owner_Id))
                .ToList();

            var cartNotChecked = cartCollection.FirstOrDefault(y => !y.IsChecked);
            var isCheckedAll = cartNotChecked is null;

            cartCollection!.ForEach(y =>
            {
                y.IsChecked = !isCheckedAll;
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}