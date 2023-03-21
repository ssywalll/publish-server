using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Banner  : BaseAuditableEntity
    {
        public string? Subject { get; set; }
        public string? Link { get; set; }
        public string? ImagePath { get; set;}
    }
}