using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration
        : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Email)
                .IsRequired();
            builder.HasIndex(a => a.Email)
                .IsUnique();
            builder.Property(a => a.UserName)
                .IsRequired();
            builder.HasIndex(a => a.UserName)
                .IsUnique();
            builder.Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(x => x.Lastname)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(x => x.Age)
                .IsRequired();
        }
    }
}