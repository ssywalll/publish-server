using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Banners.Commands.CreateCommand;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Banners.Queries.GetBanner
{
    public record GetBannerQuery : IRequest<BannerVm>;

    public class GetBannerQueryHandler : IRequestHandler<GetBannerQuery, BannerVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBannerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BannerVm> Handle(GetBannerQuery request, CancellationToken cancellationToken)
        {
             return new BannerVm
            {
                Status = "Ok",
                Data = await _context.Banners
                    .AsNoTracking()
                    .ProjectTo<BannerDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}