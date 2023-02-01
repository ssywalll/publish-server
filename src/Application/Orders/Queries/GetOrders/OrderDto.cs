
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public DateTime Order_Time { get; set; }
        public string Meal_Date { get; set; } = string.Empty;
        public Status Status { get; set; }
        public int Bank_Number { get; set; }
        public string Payment_Url { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int User_Id { get; set; }
    }
}