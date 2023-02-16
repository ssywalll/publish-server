using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Carts.Queries.GetCarts;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Common.Exceptions;
using System.Net;

namespace CleanArchitecture.Application.Carts.Queries.ExportCarts
{
    public record GetCartByToken : IRequest<PaginatedList<CartDto>>
    {
        [FromHeader(Name = "Authorization")]
        public string? Token { get; init; }
        [FromQuery]
        public int PageNumber { get; init; } = 1;
        [FromQuery]
        public int PageSize { get; init; } = 10;
    }

    public class GetCartByTokenHandler : IRequestHandler<GetCartByToken, PaginatedList<CartDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCartByTokenHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CartDto>> Handle(GetCartByToken request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null!;

            string[] tokenSplit = request.Token!.Split(new char[] { });

            if (tokenSplit == null)
                throw new NotFoundException("Token Tidak Ada Harap Login Kembali", HttpStatusCode.BadRequest);

            var key = Encoding.UTF8.GetBytes("v8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@NcRfUjXn2r5u7x!A%D*G-KaPdSgVkYp");
            var secretKey = new SymmetricSecurityKey(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(tokenSplit[1], new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = secretKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var user = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return await _context.Carts
                    .Where(x => x.User_Id == user)
                    .AsNoTracking()
                    .ProjectTo<CartDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
            catch
            {
                throw new NotFoundException("Token Tidak Valid", HttpStatusCode.BadRequest);
            }
        }
    }
}