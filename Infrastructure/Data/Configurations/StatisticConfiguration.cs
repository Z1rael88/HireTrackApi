using Domain.Models;
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
    }
}