using AutoMapper;
using MediatR;
using System.Net;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Carts.Commands.DeleteCart
{
    public record RemoveCartMultiple : UseAprizax, IRequest { }

    public class RemoveCartMultipleHandler : IRequestHandler<RemoveCartMultiple>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RemoveCartMultipleHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveCartMultiple request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            var cartChecked = _context.Carts
                .Where(x => x.User_Id.Equals(tokenInfo.Owner_Id))
                .Where(y => y.IsChecked)
                .ToList();

            cartChecked.ForEach(z => _context.Carts.Remove(z));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}