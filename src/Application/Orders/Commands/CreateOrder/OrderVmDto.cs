using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
namespace CleanArchitecture.Application.Orders.Commands.CreateOrder
{
    public class OrderVmDto : IMapFrom<Order>
    {
        public DateTime OrderTime { get; set; }
        public DateTime MealDate { get; set; }
        public Status Status { get; set; }
        public string BankNumber { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderVmDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.users!.Name));
        }
    }
}