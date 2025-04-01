using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TechnologyConfiguration : BaseEntityConfiguration<Technology>
{
    public override void Configure(EntityTypeBuilder<Technology> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.YearsOfExperience).IsRequired();
        builder.HasOne(x=>x.TechnologyType)
            .WithMany()
            .HasForeignKey(x => x.TechnologyTypeId);
    }
}