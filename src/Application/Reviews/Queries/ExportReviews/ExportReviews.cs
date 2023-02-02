using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Reviews.Queries.GetReviews;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Reviews.Queries.ExportReviews;

    public class ExportReviewsQuery : IRequest<ReviewsVm>
    {
        public int Id { get; set; }
    }

    public class ExportReviewsQueryHandler : IRequestHandler<ExportReviewsQuery, ReviewsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportReviewsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewsVm> Handle(ExportReviewsQuery request, CancellationToken cancellationToken)
        {
             return new ReviewsVm
            {
                Data = await _context.Reviews
                    .Where(x => x.Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }

    }

