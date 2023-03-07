using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class ItemGraphDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public float TotalPriceOrdered { get; set; }
        public string? OrderTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, ItemGraphDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.users!.Name))
                .ForMember(d => d.TotalPriceOrdered, opt => opt.MapFrom(s => s.FoodDrinkOrders!.Sum(t => t.Quantity * t.FoodDrinkMenus!.Price)))
                .ForMember(d => d.OrderTime, opt => opt.MapFrom(s => s.Order_Time.GetFormattedDate()));
        }
    }
}
