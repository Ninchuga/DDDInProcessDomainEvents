using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entitites;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        //public OrderConfiguration()
        //{
        //    HasKey(o => o.Id);
            
        //    Property(o => o.Id)
        //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
        //        .IsRequired();

        //    Property(o => o.UserId)
        //        .IsRequired();

        //    Property(o => o.UserName)
        //        .IsRequired();

        //    Property(o => o.TotalPrice)
        //        .HasColumnType("decimal(18, 4)")
        //        .IsRequired();

        //    Property(o => o.OrderStatus)
        //        .IsRequired();

        //    Property(o => o.OrderDate)
        //        .IsRequired();

        //    Property(o => o.OrderCancellationDate)
        //        .IsOptional();

        //    Property(o => o.PaymentData.CardName)
        //        .HasColumnName("PaymentData_CardName")
        //        .IsRequired();

        //    Property(o => o.PaymentData.CardNumber)
        //        .HasColumnName("PaymentData_CardNumber")
        //        .IsRequired();

        //    Property(o => o.PaymentData.CVV)
        //        .HasColumnName("PaymentData_CVV")
        //        .IsRequired();

        //    Property(o => o.PaymentData.OrderPaid)
        //        .HasColumnName("PaymentData_OrderPaid")
        //        .IsRequired();

        //    HasRequired(o => o.OrderItems)
        //        .WithRequiredPrincipal()
        //        .WillCascadeOnDelete();

        //    Ignore(o => o.DomainEvents);

        //    // If its set to IsRequired EF will use cascade deleting by default
        //    //HasMany(o => o.OrderItems)
        //    //    .WithRequired(o => o.);

        //    //HasMany<OrderItem>(o => o.OrderItems)
        //    //    .WithOne()
        //    //    .IsRequired();
        //}

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
