using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class VacancyResumeConfiguration : IEntityTypeConfiguration<VacancyResume>
{
    public void Configure(EntityTypeBuilder<VacancyResume> builder)
    {
        builder
            .HasKey(vr => new { vr.VacancyId, vr.ResumeId });
        builder.Property(x => x.Status).IsRequired();
        builder
            .HasOne(vr => vr.Vacancy)
            .WithMany(v => v.VacancyResumes)
            .HasForeignKey(vr => vr.VacancyId);

        builder
            .HasOne(vr => vr.Resume)
            .WithMany(r => r.VacancyResumes)
            .HasForeignKey(vr => vr.ResumeId);
    }
}