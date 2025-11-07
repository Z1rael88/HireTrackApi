using Domain.Models;
using Microsoft.EntityFrameworkCore;
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
        builder.Property(x => x.AddDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(a => a.Salary) .IsRequired();
        builder.Property(x => x.YearsOfExperience).IsRequired();
        builder.Property(x => x.WorkType).IsRequired();

        builder.HasOne(x=>x.Hr)
            .WithMany()
            .HasForeignKey(x=>x.HrId);
        builder.HasOne(x => x.Company)
            .WithMany(x=>x.Vacancies)
            .HasForeignKey(x => x.CompanyId);
        builder.HasMany(x => x.LanguageLevelRequirements)
            .WithOne()
            .HasForeignKey(x => x.VacancyId);
        builder.HasMany(x => x.EducationsRequirements)
            .WithOne()
            .HasForeignKey(x => x.VacancyId);
        builder.HasMany(x => x.JobExperienceRequirements)
            .WithOne()
            .HasForeignKey(x => x.VacancyId);
        
        builder.OwnsOne(x => x.Address, a =>
        {
            a.Property(a => a.Country).HasColumnName("Country");
            a.Property(a => a.City).HasColumnName("City");
        });
    }
}