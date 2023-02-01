using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CleanArchitecture.Application.Carts.Commands.CreateCart
{
    public record CreateCartCommand : IRequest<Cart>
    {
        public string Token { get; init; } = string.Empty;
        public int Food_Drink_Id { get; init; }
        public int Quantity { get; init; }
    }
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand ,Cart>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCartCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
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


                var entity = new Cart
                {
                        User_Id = user,
                        Food_Drink_Id = request.Food_Drink_Id,
                        Quantity = request.Quantity
                };


                _context.Carts.Add(entity);

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