using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Common
{
    public class ResultResponse
    {
        public int Id { get; set; }

        private readonly List<BaseResponse> _responses = new();

        [NotMapped]
        private string Status; // field

        public string status   // property
        {
            get { return status; }   // get method
            set { status = "Ok"; }  // set method
        }

    }
}