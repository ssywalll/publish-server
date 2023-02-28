using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.BankAccounts.Commands.CreateBankAccount
{
    public record CreateBankAccountCommand : UseAprizax, IRequest
    {
        public string BankNumber { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string BankName { get; init; } = string.Empty;

    }

    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public CreateBankAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var bankCount = await _context.BankAccounts
                    .Where(x => x.UserId.Equals(tokenInfo.Owner_Id))
                    .AsNoTracking()
                    .CountAsync(cancellationToken);

            if (bankCount >= 2)
                throw new NotFoundException("Bank yang anda miliki telah melewati batas", HttpStatusCode.BadRequest);

            var entity = new BankAccount
            {
                BankNumber = request.BankNumber,
                Name = request.Name.ToUpper(),
                BankName = request.BankName.ToUpper(),
                UserId = tokenInfo.Owner_Id ?? 0
            };

            _context.BankAccounts.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}