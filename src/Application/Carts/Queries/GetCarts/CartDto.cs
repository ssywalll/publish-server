using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Carts.Queries.GetCarts
{
    public class CartDto : IMapFrom<Cart>
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Food_Drink_Id { get; set; }
        public int Quantity { get; set; }
    }
}