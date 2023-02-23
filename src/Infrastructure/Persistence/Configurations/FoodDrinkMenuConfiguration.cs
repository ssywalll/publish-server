// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using CleanArchitecture.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace CleanArchitecture.Infrastructure.Persistence.Configurations
// {
//     public class FoodDrinkMenuConfiguration : IEntityTypeConfiguration<FoodDrinkMenu>
//     {
//         public void Configure(EntityTypeBuilder<FoodDrinkMenu> builder)
//         {
//             builder
//                    .HasOne(a => a.Orders)
//                    .WithMany(b => b.FoodDrinkMenus)
//                    .HasForeignKey(a => a.Order_Id)
//                    .HasConstraintName("Fk_FoodDrinkMenu_Order")
//                    .OnDelete(DeleteBehavior.Cascade)
//                    .IsRequired();
//         }
//     }
// }