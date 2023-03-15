using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;
using AutoMapper;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.BankAccounts.Commands.ChooseBankAccount
{
    public record ChooseBankAccountCommand : UseAprizax, IRequest
    {
        public int Id { get; init; }
    }

    public class ChooseBankAccountCommandHandler : IRequestHandler<ChooseBankAccountCommand>
    {
        private readonly IApplicationDbContext _context;
        private IMapper _mapper;
        public ChooseBankAccountCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ChooseBankAccountCommand request, CancellationToken cancellationToken)
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

            if (entity.UserId.Equals(tokenInfo.Owner_Id) is false)
                throw new NotFoundException("Bank ini bukan milik anda", HttpStatusCode.BadRequest);

            await _context.BankAccounts
                .Where(x => x.UserId.Equals(tokenInfo.Owner_Id))
                .ForEachAsync(y =>
                {
                    y.IsChoosen = y.Id.Equals(request.Id) ? true : false;
                }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}