using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder
{
    public class PostOrderDto : IMapFrom<Order>
    {
        // public DateTime OrderTime { get; set; }
        public DateTime MealDate { get; set; }
        // public Status Status { get; set; }
        // public string Bank_Number { get; set; } = string.Empty;
        public IFormFile? Payment { get; set; }
        public string Address { get; set; } = string.Empty;
        // public int BankAccount_Id { get; set; }
    }
}