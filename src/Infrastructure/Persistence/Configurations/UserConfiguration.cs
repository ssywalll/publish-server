using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(n => n.Name)
                    .HasMaxLength(200);
            builder.Property(n => n.Email)
                    .HasMaxLength(200);
            builder.Property(n => n.Password)
                    .HasMaxLength(200);
            builder.Property(n => n.Phone)
                    .HasMaxLength(200);




        }

    }
}