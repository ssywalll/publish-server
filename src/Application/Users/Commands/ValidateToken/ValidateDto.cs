using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Users.Commands.ValidateToken
{
    public class ValidateDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Phone { get; set ;}
        public Gender Gender { get; set; }
        public string Picture_Url { get; set; } = string.Empty;
    }
}