using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Users.Commands.ValidateToken
{
    public record ValidateToken : IRequest<ValidateVm>
    {
        public string Token { get; init; } = string.Empty;
    }

    public class ValidateTokenHandler : IRequestHandler<ValidateToken, ValidateVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ValidateTokenHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ValidateVm> Handle(ValidateToken request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null!;

            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey = new SymmetricSecurityKey(key);
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


                return new ValidateVm
                {
                    Status = "Ok",
                    data = await _context.Users
                        .Where(x => x.Id == user)
                        .AsNoTracking()
                        .ProjectTo<ValidateDto>(_mapper.ConfigurationProvider)
                        .SingleOrDefaultAsync(cancellationToken)
                };
            }
            catch
            {
                return new ValidateVm
                {
                    Status = "Expired",
                    data = null,
                };
            }
        }
    }
}