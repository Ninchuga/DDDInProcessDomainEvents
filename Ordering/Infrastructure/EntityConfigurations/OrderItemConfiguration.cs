using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entitites;
using System;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        //public OrderItemConfiguration()
        //{
        //    HasKey(i => i.Id);

        //    Property(i => i.Id)
        //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        //    // shadow property (FK) for Order entity Id
        //    //Property<Guid>("OrderId")
        //    //    .IsRequired();

        //    Property(i => i.Discount)
        //        .HasColumnType("decimal(18, 2)")
        //        .IsRequired();

        //    Property(i => i.ProductId)
        //        .IsRequired();

        //    Property(i => i.ProductName)
        //        .IsRequired();

        //    Property(i => i.Price)
        //        .HasColumnType("decimal(18, 4)")
        //        .IsRequired();

        //    Property(i => i.Quantity)
        //        .IsRequired();

        //    Ignore(item => item.PriceWithDiscount);
        //}

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            // shadow property (FK) for Order entity Id
            builder.Property<Guid>("OrderId")
                .IsRequired();

            builder
                .Property(i => i.Discount)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(i => i.ProductId)
                .IsRequired();

            builder
                .Property(i => i.ProductName)
                .IsRequired();

            builder
                .Property(i => i.Price)
                .HasColumnType("decimal(18, 4)")
                .IsRequired();

            builder
                .Property(i => i.Quantity)
                .IsRequired();

            builder.Ignore(item => item.PriceWithDiscount);
        }
    }
}
