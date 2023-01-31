using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Tags.Queries.GetTags;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tags.Queries.ExportTags;

    public class ExportTagsQuery : IRequest<TagsVm>
    {
        public int Id { get; set; }
    }

    public class ExportTagsQueryHandler : IRequestHandler<ExportTagsQuery, TagsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagsVm> Handle(ExportTagsQuery request, CancellationToken cancellationToken)
        {
             return new TagsVm
            {
                Data = await _context.Tags
                    .Where(x => x.Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }

    }

