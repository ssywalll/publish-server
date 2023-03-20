using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Common.Context;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class DataGraphDto : IMapFrom<Order>
    {
        public int TotalOrder { get; set; }
        public string? OrderTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, DataGraphDto>()
                .ForMember(d => d.OrderTime, opt => opt.MapFrom(s => s.Order_Time.GetFormattedDateGraph()))
                .ForMember(d => d.TotalOrder, opt => opt.MapFrom(s => TotalOrder + s.FoodDrinkOrders!.Sum(t => t.Quantity)));
        }
    }
    public class DataGraphDto2 : IMapFrom<Order>
    {
        public float IncomeOrder { get; set; }
        public float IncomeComparison { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, DataGraphDto2>()
                .ForMember(d => d.IncomeOrder, opt => opt.MapFrom(s => s.FoodDrinkOrders!.Sum(t => t.Quantity * t.FoodDrinkMenus!.Price)))
                .ForMember(d => d.IncomeComparison, opt => opt.MapFrom(s => s.FoodDrinkOrders!.Sum(t => t.Quantity * t.FoodDrinkMenus!.Price)));
        }
    }
}