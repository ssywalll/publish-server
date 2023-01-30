using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.BankAccounts.Commands.DeleteBankAccount
{
    public record DeleteBankAccountCommand(int Id) : IRequest;
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBankAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BankAccounts
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(BankAccounts), request.Id);
            }

            _context.BankAccounts.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }


    }
}