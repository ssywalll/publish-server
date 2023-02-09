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
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Application.Common.Context;

namespace CleanArchitecture.Application.Users.Commands.ValidateToken
{
    public record ValidateToken : UseAprizax, IRequest<ValidateVm>
    {
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

            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                return new ValidateVm
                {
                    Status = "Expired",
                    data = null,
                };
            var userData = await _context.Users
                    .Where(x => x.Id == tokenInfo.Owner_Id)
                    .AsNoTracking()
                    .ProjectTo<ValidateDto>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken);
            return new ValidateVm
            {
                Status = "Ok",
                data = userData
            };
        }
    }
}