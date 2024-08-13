using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Table.Domain;

namespace Infrastructure.Configurations.Table;

public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(40);
        builder.Property(x => x.Address).HasMaxLength(60);

        builder
            .HasMany(x => x.Tables)
            .WithOne(x => x.Place)
            .HasForeignKey(x => x.PlaceId);
    }
}