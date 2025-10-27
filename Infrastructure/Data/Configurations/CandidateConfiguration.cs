using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CandidateConfiguration : BaseEntityConfiguration<Candidate>
{
    public override void Configure(EntityTypeBuilder<Candidate> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Firstname).IsRequired();
        builder.Property(x => x.Lastname).IsRequired();
        builder.Property(x => x.Bio).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.WorkType).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne<User>(x=>x.User)
            .WithOne()
            .HasForeignKey<Candidate>(x => x.UserId)
            .IsRequired(false);

        builder.OwnsOne(x => x.Address, a =>
        {
            a.Property(a => a.Country).HasColumnName("Country");
            a.Property(a => a.City).HasColumnName("City");
        });
    }
}