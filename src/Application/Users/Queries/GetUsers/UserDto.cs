using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Users.Queries.GetUsers
{
    public record UserDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public int Phone { get; set; }
        public Gender Gender { get; set; }
        public string Picture_Url { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public float TotalPriceOrdered { get; set; }

         public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Orders!.Sum(x => x.FoodDrinkOrders!.Sum(y => y.Quantity))))
                .ForMember(d => d.TotalPriceOrdered, opt => opt.MapFrom(s => s.Orders!.Sum(x => x.FoodDrinkOrders!.Sum(y => y.Quantity * y.FoodDrinkMenus!.Price))));
        }
    }
}