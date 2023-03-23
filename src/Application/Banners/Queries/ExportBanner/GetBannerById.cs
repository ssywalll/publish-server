using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Banners.Queries.GetBanner;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Banners.Queries.ExportBanner
{
    public record GetBannerById : IRequest<BannerDto>
    {
        public int Id { get; set; }
    }

    public class GetBannerByIdHandler : IRequestHandler<GetBannerById, BannerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBannerByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BannerDto> Handle(GetBannerById request, CancellationToken cancellationToken)
        {
            var entity = await _context.Banners
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .ProjectTo<BannerDto>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return entity;
        }
    }
}