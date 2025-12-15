using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class StatisticsConfiguration : BaseEntityConfiguration<Statistics>
{
    public override void Configure(EntityTypeBuilder<Statistics> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ResumeId).IsRequired();
        builder.Property(x => x.VacancyId).IsRequired();
        builder.Property(x => x.TotalMatchPercent).IsRequired();
        builder.Property(x => x.EducationMatchPercent).IsRequired();
        builder.Property(x => x.EducationSummary).IsRequired();
        builder.Property(x => x.LanguageMatchPercent).IsRequired();
        builder.Property(x => x.LanguageSummary).IsRequired();
        builder.Property(x => x.ExperienceMatchPercent).IsRequired();
        builder.Property(x => x.ExperienceSummary).IsRequired();
        builder.HasOne(x => x.Resume)
            .WithMany()
            .HasForeignKey(x => x.ResumeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Vacancy)
            .WithMany()
            .HasForeignKey(x => x.VacancyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Summary, a =>
        {
            a.Property(a => a.EducationSummary).HasColumnName("EducationSummary");
            a.Property(a => a.ExperienceSummary).HasColumnName("ExperienceSummary");
            a.Property(a => a.LanguageLevelSummary).HasColumnName("LanguageLevelSummary");
            a.Property(a => a.TotalSummary).HasColumnName("TotalSummary");
        });
    }
}