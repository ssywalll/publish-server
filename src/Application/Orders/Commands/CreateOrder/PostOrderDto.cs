using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder
{
    public class PostOrderDto : IMapFrom<Order>
    {
        public DateTime Order_Time { get; set; }
        public DateTime Meal_Date { get; set; }
        public Status Status { get; set; }
        public string Bank_Number { get; set; } = string.Empty;
        public string Payment_Url { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int BankAccount_Id { get; set; }
    }
}