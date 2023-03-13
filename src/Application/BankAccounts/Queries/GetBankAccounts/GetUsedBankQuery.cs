using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Carts.Queries.GetCarts;

namespace CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts
{
    public record GetUsedBankQuery : UseAprizax, IRequest<UsedBankVm>;

    public class GetUsedBankQueryHandler : IRequestHandler<GetUsedBankQuery, UsedBankVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsedBankQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsedBankVm> Handle(GetUsedBankQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var usedBank = await _context.BankAccounts
                .Where(x => x.UserId.Equals(tokenInfo.Owner_Id) && x.IsChoosen)
                .AsNoTracking()
                .ProjectTo<CheckoutBankDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            return new UsedBankVm
            {
                Status = "Ok",
                Data = usedBank
            };
        }
    }
}