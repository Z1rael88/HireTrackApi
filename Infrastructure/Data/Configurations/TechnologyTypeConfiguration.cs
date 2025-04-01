using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TechnologyTypeConfiguration : BaseEntityConfiguration<TechnologyType>
{
    public override void Configure(EntityTypeBuilder<TechnologyType> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.LogoUrl).IsRequired();
        builder.Property(x => x.TechnologyCategory).IsRequired();
    }
}