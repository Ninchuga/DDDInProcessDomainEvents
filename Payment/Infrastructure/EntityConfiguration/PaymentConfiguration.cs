using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Payment.Infrastructure.EntityConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Domain.Payment>
    {
        //public PaymentConfiguration()
        //{
        //    HasKey(p => p.Id);

        //    Property(p => p.Id)
        //        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        //    Property(p => p.CardName)
        //        .IsRequired();

        //    Property(p => p.CardNumber)
        //        .IsRequired();

        //    Property(p => p.OrderId)
        //        .IsRequired();

        //    Property(p => p.OrderPaid)
        //        .IsRequired();

        //    Property(p => p.CVV)
        //        .IsRequired();

        //    Property(p => p.OrderTotalPrice)
        //        .IsRequired();
        //}

        public void Configure(EntityTypeBuilder<Domain.Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(p => p.CardName)
                .IsRequired();
            
            builder.Property(p => p.CardNumber)
                .IsRequired();
            
            builder.Property(p => p.OrderId)
                .IsRequired();
            
            builder.Property(p => p.OrderPaid)
                .IsRequired();
            
            builder.Property(p => p.CVV)
                .IsRequired();
            
            builder.Property(p => p.OrderTotalPrice)
                .IsRequired();
        }
    }
}
