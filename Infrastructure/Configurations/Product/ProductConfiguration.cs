using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Infrastructure.Configurations.Product;

public class ProductConfiguration : IEntityTypeConfiguration<global::Product.Domain.Product>
{
    public void Configure(EntityTypeBuilder<global::Product.Domain.Product> builder)
    {
        builder.Ignore(d => d.DomainEvents);

        builder.Property(x => x.Name).HasMaxLength(20);

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);

        builder
            .HasMany(x => (IEnumerable<OrderItem>)x.OrderItems)
            .WithOne(x => (global::Product.Domain.Product)x.Product)
            .HasForeignKey(x => x.ProductId);
    }
}