using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Order;

public class OrderConfiguration : IEntityTypeConfiguration<global::Order.Domain.Order>
{
    public void Configure(EntityTypeBuilder<global::Order.Domain.Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId);

        builder
            .HasOne(x => (global::Table.Domain.Table)x.Table)
            .WithMany(x => (IEnumerable<global::Order.Domain.Order>)x.Orders)
            .HasForeignKey(x => x.TableId);
    }
}