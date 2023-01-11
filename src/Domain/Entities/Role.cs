using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public int User_Id { get; set ; }
    }
}