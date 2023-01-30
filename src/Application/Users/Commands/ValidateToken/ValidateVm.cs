using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Users.Commands.ValidateToken
{
    public class ValidateVm
    {
        public string Status { get; set; } = string.Empty;

        public ValidateDto data { get; set; } = new ValidateDto();
    }
}