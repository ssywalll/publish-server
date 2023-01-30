using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Users.Commands.Login
{
    public record LoginCommand : IRequest<LoginVm>
    {
        public string Email { get; init; } = string.Empty; 
        public string Password { get; init; } = string.Empty;
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public LoginCommandHandler(IApplicationDbContext context, IMapper mapper, IOptions<JwtSettings> options)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = options.Value;
        }

        public async Task<LoginVm> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .Where(x => x.Email == request.Email)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException();
            }

            bool isAuth = BCrypt.Net.BCrypt.Verify(request.Password, entity.Password);

            if(isAuth == false)
            {
                throw new NotFoundException();
            }

            //Generate Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtSettings.securitykey);
            var tokendesc = new SecurityTokenDescriptor
            {
               Subject = new ClaimsIdentity(
                    new Claim[] 
                    { 
                        new Claim("id", entity.Id.ToString()),
                        new Claim(ClaimTypes.Name, entity.Email),
                        new Claim(ClaimTypes.Role, entity.Role)
                    }
                ),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokendesc);
            string finalToken = tokenHandler.WriteToken(token);

            return new LoginVm
            {
                Status = "Ok",
                Token = finalToken,
                data = await _context.Users
                    .Where(x => x.Email == entity.Email)
                    .AsNoTracking()
                    .ProjectTo<LoginDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken)
            };
        }
    }
}