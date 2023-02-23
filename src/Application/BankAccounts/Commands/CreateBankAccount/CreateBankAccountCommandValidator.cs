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

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("User Id is Required.")
                .NotNull();
        }
    }
}