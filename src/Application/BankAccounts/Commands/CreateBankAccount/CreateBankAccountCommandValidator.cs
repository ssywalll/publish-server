using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.BankAccounts.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankAccountCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        // public async Task<int> CekId(int id, CancellationToken cancellationToken)
        // {
        //     return await _context.BankAccounts
        //         .AllAsync(l => l.User_Id != User_id, cancellationToken);
        // }
    }
}