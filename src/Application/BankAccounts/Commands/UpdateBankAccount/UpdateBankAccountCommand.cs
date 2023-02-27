using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;
using AutoMapper;
using System.Net;

namespace CleanArchitecture.Application.BankAccounts.Commands.UpdateBankAccount
{
    public record UpdateBankAccountCommand : UseAprizax, IRequest
    {
        public int Id { get; init; }
        public string BankNumber { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string BankName { get; init; } = string.Empty;
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

            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var entity = await _context.BankAccounts
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException("Bank tidak ditemukan", HttpStatusCode.BadRequest);

            if (entity.User_Id.Equals(tokenInfo.Owner_Id) is false)
                throw new NotFoundException("Bank ini bukan milik anda", HttpStatusCode.BadRequest);

            entity.Bank_Number = request.BankNumber;
            entity.Name = request.Name.ToUpper();
            entity.Bank_Name = request.BankName.ToUpper();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}