using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Banners.Queries.GetBanner
{
    public class BannerDto : IMapFrom<Banner>
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Link { get; set; }
        public string? ImagePath { get; set; }
    }
}