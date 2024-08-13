using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Infrastructure.Configurations.Order;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.OrderId);

        builder
            .HasOne(x => (global::Product.Domain.Product)x.Product)
            .WithMany(x => (IEnumerable<OrderItem>?)x.OrderItems)
            .HasForeignKey(x => x.ProductId);
    }
}