using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<User> Users { get; }

    DbSet<BankAccount> BankAccounts { get; }

    DbSet<Cart> Carts { get; }

    DbSet<FoodDrinkMenu> FoodDrinkMenus { get; }

    DbSet<FoodDrinkOrder> FoodDrinkOrders { get; }

    DbSet<JwtSettings> JwtSettings { get; }

    DbSet<Order> Orders { get; }

    DbSet<Review> Reviews { get; }

    DbSet<Tag> Tags { get; }

    DbSet<Banner> Banners { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
