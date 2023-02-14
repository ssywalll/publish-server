using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Users.Queries.GetUsers;

namespace CleanArchitecture.Application.Users.Commands.Login
{
    public class LoginVm
    {
        public string Status { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public UserDto? data { get; set; } = new UserDto();
    }
}