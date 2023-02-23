
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string? OrderTime { get; set; }
        public string? MealDate { get; set; }
        public Status Status { get; set; }
        public string? BankNumber { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int BankAccountId { get; set; }
        public string? Name { get; set; }
        public string? BankName { get; set; }
        public float TotalPriceOrdered { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.BankAccounts!.Name))
                .ForMember(d => d.BankName, opt => opt.MapFrom(s => s.BankAccounts!.Bank_Name))
                .ForMember(d => d.BankNumber, opt => opt.MapFrom(s => s.BankAccounts!.Bank_Number))
                .ForMember(d => d.OrderTime, opt => opt.MapFrom(s => s.Order_Time.GetFormattedDate()))
                .ForMember(d => d.MealDate, opt => opt.MapFrom(s => s.Meal_Date.GetFormattedDate()))
                .ForMember(d => d.TotalPriceOrdered, opt => opt.MapFrom(s => s.FoodDrinkOrders!.Sum(t => t.Quantity * t.FoodDrinkMenus!.Price)));
        }
    }
}