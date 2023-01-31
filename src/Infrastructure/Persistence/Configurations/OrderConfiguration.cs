using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(n => n.Meal_Date)
         .HasMaxLength(25);
        builder.Property(n => n.Address)
         .HasMaxLength(500);

        builder
         .HasOne(r => r.users)
         .WithMany(u => u.Orders)
         .HasForeignKey(t => t.User_Id)
         .HasConstraintName("Fk_Order_User")
         .OnDelete(DeleteBehavior.Cascade)
         .IsRequired();

        builder
          .HasOne(r => r.BankAccounts)
          .WithMany(c => c.Orders)
          .HasForeignKey(c => c.BankAccount_Id)
          .HasConstraintName("Fk_Order_BankAccount")
          .OnDelete(DeleteBehavior.Cascade)
          .IsRequired();


    }
}
