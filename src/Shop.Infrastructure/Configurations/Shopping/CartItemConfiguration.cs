using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shop.Infrastructure.Configurations.Shopping;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.ProductId).IsRequired();

        builder.Property(ci => ci.CartId).IsRequired();

        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ci => new { ci.ProductId, ci.CartId })
            .IsUnique();

        builder.HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId);

        builder.Property(ci => ci.Price)
            .HasPrecision(18, 2);
    }
}
