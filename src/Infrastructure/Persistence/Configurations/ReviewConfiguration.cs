using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(n => n.Comment)
         .HasMaxLength(500);

        builder
         .HasOne(r => r.User)
         .WithMany(u => u.Reviews)
         .HasForeignKey(t => t.User_Id)
         .HasConstraintName("Fk_Review_User")
         .OnDelete(DeleteBehavior.Cascade)
         .IsRequired();

        builder
        .HasOne(t => t.FoodDrinkMenu)
        .WithMany(c => c.Reviews)
        .HasForeignKey(t => t.Food_Drink_Id)
        .HasConstraintName("Fk_Review_FoodDrinkMenu")
        .OnDelete(DeleteBehavior.Cascade)
        .IsRequired();



    }
}
