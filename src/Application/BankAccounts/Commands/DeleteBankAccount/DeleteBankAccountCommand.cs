using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace CleanArchitecture.Application.BankAccounts.Commands.DeleteBankAccount
{
    public record DeleteBankAccountCommand : UseAprizax, IRequest
    {
        public int Id { get; init; }
    }
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBankAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var entity = await _context.BankAccounts
            .Where(x => x.Id.Equals(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

            if (entity is null)
                throw new NotFoundException("Bank tidak ditemukan", HttpStatusCode.BadRequest);

            if (entity.User_Id.Equals(tokenInfo.Owner_Id) is false)
                throw new NotFoundException("Bank ini bukan milik anda", HttpStatusCode.BadRequest);

            _context.BankAccounts.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}