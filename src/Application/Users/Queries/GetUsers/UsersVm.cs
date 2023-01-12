using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Users.Queries.GetUsers
{
    public class UsersVm
    {
        public string Status { get; set; } = string.Empty;
        public IList<UserDto> Data { get; set; } = new List<UserDto>();
    }
}