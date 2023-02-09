using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder
{
    public class PostCartDto : IMapFrom<Cart>
    {
        public int Food_Drink_Id { get; set; }
        public int Quantity { get; set; }
    }
}