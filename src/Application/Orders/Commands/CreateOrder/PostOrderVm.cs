using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder
{
    public class PostOrderVm
    {
        public string? Status { get; set; }
        public OrderVmDto? OrdersData { get; set; } 
    }
}