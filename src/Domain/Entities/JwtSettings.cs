using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class JwtSettings : BaseAuditableEntity
    {
        public string securitykey { get; set; }
    }
}