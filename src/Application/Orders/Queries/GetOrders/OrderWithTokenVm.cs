// using MediatR;
using CleanArchitecture.Domain.Entities;
// using CleanArchitecture.Application.Common.Interfaces;
// using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Mappings;
using AutoMapper;
// using System.Net;
// using Microsoft.EntityFrameworkCore;
// using AutoMapper.QueryableExtensions;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Application.Carts.Queries.GetCarts;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders;

public record FoodDrinkOrderDto : IMapFrom<FoodDrinkOrder>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public float Price { get; init; }
    public float TotalPrice { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FoodDrinkOrder, FoodDrinkOrderDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.FoodDrinkMenus!.Name))
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s.FoodDrinkMenus!.Price))
            .ForMember(d => d.TotalPrice, opt => opt.MapFrom(s => s.Quantity * s.FoodDrinkMenus!.Price));
    }
}

public record OrderWithTokenDto : IMapFrom<Order>
{
    public int Id { get; init; }
    public string? OrderTime { get; init; }
    public string? MealDate { get; init; }
    public string Address { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public float TotalPriceOrdered { get; init; }
    public Status Status { get; init; }
    public CheckoutBankDto? UsedBank { get; init; }
    public List<FoodDrinkOrderDto>? Orders { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderWithTokenDto>()
            .ForMember(d => d.OrderTime, opt => opt.MapFrom(s => s.Order_Time.GetFormattedDate()))
            .ForMember(d => d.MealDate, opt => opt.MapFrom(s => s.Meal_Date.GetFormattedDate()))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.users!.Name))
            .ForMember(d => d.TotalPriceOrdered, opt => opt.MapFrom(
                s => s.FoodDrinkOrders!.Sum(t => t.Quantity * t.FoodDrinkMenus!.Price)
            ))
            .ForMember(d => d.UsedBank, opt => opt.MapFrom(s => s.BankAccounts))
            .ForMember(d => d.Orders, opt => opt.MapFrom(s => s.FoodDrinkOrders));
    }
}

public record OrderWithTokenVm
{
    public string Status { get; init; } = string.Empty;
    public OrderWithTokenDto? Data { get; init; }
}