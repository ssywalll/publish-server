using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Users.Commands.Login
{
    public class LoginVm
    {
        public string Status { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public LoginDto? data { get; set; } = new LoginDto();
    }
}