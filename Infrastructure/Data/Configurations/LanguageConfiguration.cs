using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class LanguageConfiguration : BaseEntityConfiguration<Language>
{
    public override void Configure(EntityTypeBuilder<Language> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.LanguageLevel).IsRequired();
        builder.Property(x => x.LanguageName).IsRequired();
        builder.HasMany(x => x.ResumeLanguages)
            .WithOne(x => x.Language)
            .HasForeignKey(x => x.LanguageId);
    }
}