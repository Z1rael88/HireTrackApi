using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        builder.Property(x => x.Email).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne<User>(x => x.User)
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