using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Orders.Queries.ExportOrder
{
    public record GetOrdersByToken : IRequest<OrderDto>
    {
        [FromHeader(Name = "Authorization")]
        public string? Token { get; init; }
    }

    public class GetOrdersByTokenHandler : IRequestHandler<GetOrdersByToken, OrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersByTokenHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrdersByToken request, CancellationToken cancellationToken)
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

                return await _context.Orders
                    .Where(x => x.User_Id == user)
                    .AsNoTracking()
                    .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                    .SingleAsync(cancellationToken);

            }
            catch
            {
                throw new NotFoundException("Token Tidak Valid", HttpStatusCode.BadRequest);
            }
        }
    }
}