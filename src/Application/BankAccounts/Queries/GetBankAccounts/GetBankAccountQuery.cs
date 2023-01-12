using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public record GetBankAccountQuery : IRequest<BankAccountsVm>;

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
            return new BankAccountsVm
            {
                Status = "Ok",
                Data = await _context.BankAccounts
                    .AsNoTracking()
                    .ProjectTo<BankAccountDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}