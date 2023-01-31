using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.Property(n => n.Quantity)
         .HasMaxLength(200);

        builder
         .HasOne(t => t.FoodDrinkMenu)
         .WithMany(c => c.Carts)
         .HasForeignKey(t => t.Food_Drink_Id)
         .HasConstraintName("Fk_Cart_FoodDrinkMenu")
         .OnDelete(DeleteBehavior.Cascade)
         .IsRequired();
    }

}