using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(n => n.Name)
           .HasMaxLength(100);

        builder
           .HasOne(t => t.FoodDrinkMenus)
           .WithMany(f => f.Tags)
           .HasForeignKey(t => t.Food_Drink_Id)
           .HasConstraintName("Fk_Tag_FoodDrinkMenu")
           .OnDelete(DeleteBehavior.Cascade)
           .IsRequired();
    }

}