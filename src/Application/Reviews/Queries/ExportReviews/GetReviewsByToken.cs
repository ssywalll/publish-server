using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Reviews.Queries.GetReviews;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Application.Reviews.Queries.ExportReviews
{
    public record GetReviewsByToken : UseAprizax, IRequest<ReviewDto>;

    public class GetReviewsByTokenHandler : IRequestHandler<GetReviewsByToken, ReviewDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewsByTokenHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(GetReviewsByToken request, CancellationToken cancellationToken)
        {
            var owner_Id = request.GetOwnerId();

            return await _context.Reviews
                .Where(x => x.User_Id == owner_Id)
                .AsNoTracking()
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}