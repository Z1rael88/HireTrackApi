using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class StatisticConfiguration : BaseEntityConfiguration<Statistic>
{
    public override void Configure(EntityTypeBuilder<Statistic> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ResumeId).IsRequired();
        builder.Property(x => x.VacancyId).IsRequired();
        builder.Property(x => x.TotalMatchPercent).IsRequired();
        builder.Property(x => x.Summary).IsRequired();
        builder.Property(x => x.EducationMatchPercent).IsRequired();
        builder.Property(x => x.EducationSummary).IsRequired();
        builder.Property(x => x.LanguageMatchPercent).IsRequired();
        builder.Property(x => x.LanguageSummary).IsRequired();
        builder.Property(x => x.ExperienceMatchPercent).IsRequired();
        builder.Property(x => x.ExperienceSummary).IsRequired();
        
        builder.OwnsOne(x => x.Summary, a =>
        {
            a.Property(a => a.EducationSummary).HasColumnName("EducationSummary");
            a.Property(a => a.ExperienceSummary).HasColumnName("ExperienceSummary");
            a.Property(a => a.LanguageLevelSummary).HasColumnName("LanguageLevelSummary");
            a.Property(a => a.TotalSummary).HasColumnName("TotalSummary");
        });
    }
}