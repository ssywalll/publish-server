using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Users.Queries.GetUsers;

namespace CleanArchitecture.Application.Users.Commands.ValidateToken
{
    public class ValidateVm
    {
        public string Status { get; set; } = string.Empty;

        public UserDto? data { get; set; } = new UserDto();
    }
}