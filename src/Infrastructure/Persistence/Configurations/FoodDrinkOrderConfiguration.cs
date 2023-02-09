using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class FoodDrinkOrderConfiguration : IEntityTypeConfiguration<FoodDrinkOrder>
{
    public void Configure(EntityTypeBuilder<FoodDrinkOrder> builder)
    {
        builder
        .HasOne(t => t.FoodDrinkMenus)
        .WithMany(c => c.FoodDrinkOrders)
        .HasForeignKey(t => t.Food_Drink_Id)
        .HasConstraintName("Fk_FoodDrinkOrder_FoodDrinkMenu")
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

        builder
        .HasOne(t => t.Orders)
        .WithMany(c => c.FoodDrinkOrders)
        .HasForeignKey(t => t.Order_Id)
        .HasConstraintName("Fk_FoodDrinkOrder_Order")
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();

    }

}