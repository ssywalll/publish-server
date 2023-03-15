using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Exceptions;
using System.Net;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public record GetBankAccountQuery : UseAprizax, IRequest<BankAccountsVm>;

    public class GetBankAccountQueryHandler : IRequestHandler<GetBankAccountQuery, BankAccountsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBankAccountQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BankAccountsVm> Handle(GetBankAccountQuery request, CancellationToken cancellationToken)
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

            return new BankAccountsVm
            {
                Status = "Ok",
                IsLimit = bankCount >= 2,
                Data = await _context.BankAccounts
                    .Where(x => x.UserId.Equals(tokenInfo.Owner_Id))
                    .AsNoTracking()
                    .ProjectTo<BankAccountDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}