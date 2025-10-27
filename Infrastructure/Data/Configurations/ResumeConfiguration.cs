using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ResumeConfiguration : BaseEntityConfiguration<Resume>
{
    public override void Configure(EntityTypeBuilder<Resume> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.YearsOfExperience).IsRequired();
        builder.Property(x => x.ExpectedSalary).IsRequired();
        builder.HasMany(x => x.LanguageLevels)
            .WithOne()
            .HasForeignKey(x => x.ResumeId);
        builder.HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey(x => x.CandidateId);
        builder.HasMany(x => x.Educations)
            .WithOne()
            .HasForeignKey(x => x.ResumeId);
    }
}