using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Response : BaseEntity
    {
        public string Status { get; set; } = string.Empty;

        //relationship
        public User User { get; set; } 
    }
}