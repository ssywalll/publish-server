using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public class CartsVm
    {
        public string Status { get; set; } = string.Empty;
        public IList<CartDto> Data { get; set; } = new List<CartDto>();
    }
}