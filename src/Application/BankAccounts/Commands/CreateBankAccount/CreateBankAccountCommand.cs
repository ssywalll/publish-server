using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.BankAccounts.Commands.CreateBankAccount
{
    public record CreateBankAccountCommand : IRequest<BankAccount>
    {
        public int Number { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Bank_User { get; init; } = string.Empty;
        public int User_Id { get; init; }
    }

    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, BankAccount>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = new BankAccount
            {
                Number = request.Number,
                Name = request.Name,
                Bank_User = request.Bank_User,
                User_Id = request.User_Id
            };

            _context.BankAccounts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}