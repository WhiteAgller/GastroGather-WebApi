using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Table;

public class TableConfiguration : IEntityTypeConfiguration<global::Table.Domain.Table>
{
    public void Configure(EntityTypeBuilder<global::Table.Domain.Table> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(40);
        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.Tables)
            .HasForeignKey(x => x.PlaceId);

        builder
            .HasMany(x => (IEnumerable<global::Order.Domain.Order>)x.Orders)
            .WithOne(x => (global::Table.Domain.Table)x.Table)
            .HasForeignKey(x => x.TableId);
    }
}