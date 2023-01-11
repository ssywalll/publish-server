using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CleanArchitecture.Domain.Common
{
    public class BaseResponse : INotification
    {
        private string Status; // field

        public string status   // property
        {
            get { return Status; }   // get method
            set { Status = value; }  // set method
        }
    }
}