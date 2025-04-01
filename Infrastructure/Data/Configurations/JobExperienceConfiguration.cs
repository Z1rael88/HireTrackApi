using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class JobExperienceConfiguration : BaseEntityConfiguration<JobExperience>
{
    public override void Configure(EntityTypeBuilder<JobExperience> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.NameOfCompany).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.HasMany(x => x.Technologies)
            .WithOne()
            .HasForeignKey(x => x.JobExperienceId);
    }
}