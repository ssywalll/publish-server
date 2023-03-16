using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Exceptions;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CleanArchitecture.Application.FoodDrinkMenus.Commands.CreateFoodDrinkMenu
{
    public record CreateFoodDrinkMenuCommand : UseAprizax, IRequest<FoodDrinkMenu>
    {
        public string Name { get; init; } = string.Empty;
        public float Price { get; init; }
        public int Min_Order { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Image_Url { get; init; } = string.Empty;
        public string Token { get; init; } = string.Empty;
    }
    public class CreateFoodDrinkMenuCommandHandler : IRequestHandler<CreateFoodDrinkMenuCommand, FoodDrinkMenu>
    {
        private readonly IApplicationDbContext _context;

        public CreateFoodDrinkMenuCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FoodDrinkMenu> Handle(CreateFoodDrinkMenuCommand request, CancellationToken cancellationToken)
        {
            if (request.Token == null)
                throw new NotFoundException("Token Tidak Ada Harap Login Kembali", HttpStatusCode.BadRequest);

            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = secretKey,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken
                (request.Token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var user_Id = jwtToken.Claims.First(x => x.Type == "id").Value;
                var Roles = jwtToken.Claims.First(x => ClaimsIdentity.DefaultRoleClaimType == "Role").Value;

                if (Roles == "admin")
                {
                    var entity = new FoodDrinkMenu
                    {
                        Name = request.Name,
                        Price = request.Price,
                        Min_Order = request.Min_Order,
                        Description = request.Description,
                        Image_Url = request.Image_Url,
                    };

                    return await Aprizax.Insert<FoodDrinkMenu>
                    (_context, _context.FoodDrinkMenus, entity, cancellationToken);

                }
                return Unauthorized("You don't have to correct permissons to do this.");
            }
            catch
            {
                throw new NotFoundException("Token Tidak Ada Harap Login Kembali", HttpStatusCode.BadRequest);
            }




            //konteks, tabel konteks, record baru, cancelationToken
        }

        private FoodDrinkMenu Unauthorized(string v)
        {
            throw new NotImplementedException();
        }
    }
}