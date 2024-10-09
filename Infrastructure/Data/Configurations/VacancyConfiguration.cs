using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class VacancyConfiguration
    : BaseEntityConfiguration<Vacancy>
{
    public override void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Name)
            .HasMaxLength(70)
            .IsRequired();
        builder.HasIndex(a => a.Name)
            .IsUnique();
        builder.Property(a => a.Description)
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(x => x.AddDate)
            .IsRequired();
        builder.Property(x => x.EndDate)
            .IsRequired();
        builder.Property(a => a.Salary)
            .IsRequired();
    }
}