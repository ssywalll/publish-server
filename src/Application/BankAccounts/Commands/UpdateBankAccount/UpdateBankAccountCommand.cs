using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using System.ComponentModel.DataAnnotations;


namespace CleanArchitecture.Application.BankAccounts.Commands.UpdateBankAccount
{
    public class UpdateBankAccountCommand : IRequest
    {
        public int Id { get; init; }
        public string Bank_Number { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Bank_Name { get; init; } = string.Empty;
        public int User_Id { get; init; }
    }

    public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand>
    {
        private readonly IApplicationDbContext _context;
        private IMapper _mapper;

        public UpdateBankAccountCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.BankAccounts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(BankAccounts), request.Id);
            }

            entity.Bank_Number = request.Bank_Number;
            entity.Name = request.Name;
            entity.Bank_Name = request.Bank_Name;
            entity.User_Id = request.User_Id;


            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }


    }
}