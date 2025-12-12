using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        builder.Property(a => a.Description)
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(x => x.AddDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(a => a.Salary) .IsRequired();
        builder.Property(x => x.WorkType)
            .HasConversion(
                v => v.Select(x => (int)x).ToArray(),
                v => v.Select(x => (WorkType)x).ToList()
            )
            .Metadata.SetValueComparer(
                new ValueComparer<List<WorkType>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                ));
        builder.Property(x => x.Responsibilities).IsRequired();

        builder.HasOne(x=>x.Hr)
            .WithMany()
            .HasForeignKey(x=>x.HrId);
        builder.HasOne(x => x.Company)
            .WithMany(x=>x.Vacancies)
            .HasForeignKey(x => x.CompanyId);
        builder.HasMany(x => x.LanguageLevelRequirements)
            .WithOne()
            .HasForeignKey(x => x.VacancyId);
        
        
        builder.OwnsOne(x => x.Address, a =>
        {
            a.Property(a => a.Country).HasColumnName("Country");
            a.Property(a => a.City).HasColumnName("City");
        });
    }
}