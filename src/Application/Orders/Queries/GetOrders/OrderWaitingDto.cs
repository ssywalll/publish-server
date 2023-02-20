using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class OrderWaitingDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int Quantity { get; set; }
        public float? TotalPriceOrdered { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderWaitingDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.users!.Name))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(
                    s => s.FoodDrinkOrders!
                    .Where(t => t.Order_Id == s.Id)
                    .Sum(u => u.Quantity)
                ));
                // .ForMember(d => d.TotalPriceOrdered, opt => opt.MapFrom(
                //     s => s.FoodDrinkOrders!
                //     .Where(t => t.Order_Id == Id)
                //     .Sum(u => u.Quantity * u.Price)));
        }
    }
}