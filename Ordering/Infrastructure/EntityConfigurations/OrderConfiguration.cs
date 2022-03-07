using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entitites;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(o => o.UserId)
                .IsRequired();

            builder
                .Property(o => o.UserName)
                .IsRequired();

            builder
                .Property(o => o.UserEmail)
                .IsRequired();

            builder
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18, 4)")
                .IsRequired();

            builder
                .Property(o => o.OrderStatus)
                .HasConversion<string>()
                .IsRequired();

            builder
                .Property(o => o.OrderDate)
                .IsRequired();

            builder
                .Property(o => o.OrderCancellationDate)
                .IsRequired(false);

            // If its set to IsRequired EF will use cascade deleting by default
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .IsRequired();

            var orderItemsNavigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            orderItemsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            // Configures a relationship where the PaymentData is owned by (or part of) Order.
            builder.OwnsOne(
                order => order.PaymentData);

            builder.Ignore(o => o.DomainEvents);
        }
    }
}
