using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.BankAccounts.Commands.CreateBankAccount
{
    public record CreateBankAccountCommand : IRequest<BankAccount>
    {
        public string? Token { get; init; }
        public string Bank_Number { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Bank_Name { get; init; } = string.Empty;
    }

    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, BankAccount>
    {
        private readonly IApplicationDbContext _context;

        public CreateBankAccountCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                var entity = new BankAccount
                {
                    Bank_Number = request.Bank_Number,
                    Name = request.Name,
                    Bank_Name = request.Bank_Name,
                    User_Id = userId
                };

                _context.BankAccounts.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch
            {
                return null!;
            }
        }
    }
}