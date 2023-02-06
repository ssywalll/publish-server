using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.BankAccounts.Queries.GetBankAccounts;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.BankAccounts.Queries.ExportBankAccounts.cs
{
    public record GetBankAccountByToken : IRequest<BankAccountDto>
    {
        [FromHeader]
        public string? Token { get; init; }
    }

    public class GetBankAccountByTokenHandler : IRequestHandler<GetBankAccountByToken, BankAccountDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBankAccountByTokenHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BankAccountDto> Handle(GetBankAccountByToken request, CancellationToken cancellationToken)
        {
            if(request == null)
                return null!;

            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey =  new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = secretKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var user = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                
              return await _context.BankAccounts
                    .Where(x => x.User_Id == user)
                    .AsNoTracking()
                    .ProjectTo<BankAccountDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);

            }
            catch
            {
               throw new NotFoundException("Token Tidak Valid", HttpStatusCode.BadRequest);
            }
        }
    }
}