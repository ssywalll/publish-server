using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Users.Commands.CreateUser;
namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(this CreateUserCommand newUser, IApplicationDbContext _context)
        {
            var isExist = _context.Users.FirstOrDefault(
                user => user.Email == newUser.Email
                );
            if (isExist is null) return true;
            return false;
        }
    }
}