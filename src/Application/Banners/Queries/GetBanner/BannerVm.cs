using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Banners.Queries.GetBanner
{
    public class BannerVm
    {
        public string? Status { get; set; }
        public IList<BannerDto> Data { get; set; } = new List<BannerDto>();
    }
}