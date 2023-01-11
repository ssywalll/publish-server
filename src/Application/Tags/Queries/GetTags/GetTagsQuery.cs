using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Tags.Queries.GetTags
{
    public record GetTagsQuery : IRequest<TagsVm>;

    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, TagsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagsVm> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return new TagsVm
            {
                Data =  await _context.Tags
                    .AsNoTracking()
                    .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}