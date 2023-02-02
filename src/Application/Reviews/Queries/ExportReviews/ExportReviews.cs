using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Reviews.Queries.GetReviews;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Mappings;

namespace CleanArchitecture.Application.Reviews.Queries.ExportReviews;

    public record ExportReviewsQuery : IRequest<PaginatedList<ReviewDto>>
    {
        public int Id { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class ExportReviewsQueryHandler : IRequestHandler<ExportReviewsQuery, PaginatedList<ReviewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportReviewsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReviewDto>> Handle(ExportReviewsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .Where(x => x.Food_Drink_Id == request.Id)
                .AsNoTracking()
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }

