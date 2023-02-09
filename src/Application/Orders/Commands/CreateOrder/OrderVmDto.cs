using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder
{
    public class OrderVmDto : IMapFrom<Order>
    {
        public DateTime Order_Time { get; set; }
        public DateTime Meal_Date { get; set; }
        public string? Status { get; set; }
        public string? Bank_Number { get; set; }
        public string Payment_Url { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int BankAccount_Id { get; set; }
        public string? User_Name { get; set; }
    }
}