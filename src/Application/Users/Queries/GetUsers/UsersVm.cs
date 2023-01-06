using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Users.Queries.GetUsers
{
    public class UsersVm
    {
         public IList<UserDto> UserDtos { get; set; } = new List<UserDto>();
    }
}