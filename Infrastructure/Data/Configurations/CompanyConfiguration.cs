using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CompanyConfiguration : BaseEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.BusinessDomain).IsRequired();
        builder.Property(x => x.Description).IsRequired();
    }
}