using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Banners.Commands.CreateCommand
{
    public class CreateBannerVm
    {
        public string? Status { get; set; }
        public Banner? Data { get; set; }
    }
}