using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}