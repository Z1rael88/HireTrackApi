using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ResumeLanguageConfiguration : IEntityTypeConfiguration<ResumeLanguage>
{
    public void Configure(EntityTypeBuilder<ResumeLanguage> builder)
    {
        builder.HasKey(x => new { x.ResumeId, x.LanguageId });
        builder.HasOne(x => x.Language)
            .WithMany(x => x.ResumeLanguages)
            .HasForeignKey(x => x.LanguageId);
        builder.HasOne(x => x.Resume)
            .WithMany(x => x.ResumeLanguages)
            .HasForeignKey(x => x.ResumeId);
    }
}