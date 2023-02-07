using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Users.Queries.GetUsers;

namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public record RegisterVm
    {
        public string Status { get; set; } = string.Empty;
        public UserDto? Data { get; set; }
    }
}