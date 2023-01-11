using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Reviews.Queries.GetReviews
{
    public record GetReviewsQuery : IRequest<ReviewsVm>;

    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, ReviewsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewsVm> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            return new ReviewsVm
            {
                Data = await _context.Reviews
                    .AsNoTracking()
                    .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}