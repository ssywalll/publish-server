using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public class CartsVm
    {
        public IList<CartDto> Data { get; set; } = new List<CartDto>();
    }
}