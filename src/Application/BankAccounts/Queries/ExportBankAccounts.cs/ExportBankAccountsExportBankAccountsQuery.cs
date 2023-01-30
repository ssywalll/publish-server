using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts;

namespace CleanArchitecture.Application.BankAccounts.Queries.ExportBankAccounts;

public record ExportBankAccountsQuery : IRequest<BankAccountsVm>
{
    public int Id { get; set; }
}

public class ExportBankAccountsQueryHandler : IRequestHandler<ExportBankAccountsQuery, BankAccountsVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ExportBankAccountsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public int Id { get; private set; }

    public async Task<BankAccountsVm> Handle(ExportBankAccountsQuery request, CancellationToken cancellationToken)
    {
        return new BankAccountsVm
        {
            Data = await _context.BankAccounts
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .ProjectTo<BankAccountDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };

    }

}
