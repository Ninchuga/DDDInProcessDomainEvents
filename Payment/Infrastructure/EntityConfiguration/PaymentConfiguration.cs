using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Payment.Infrastructure.EntityConfiguration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Domain.Entities.Payment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Payment> builder)
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

            builder.Ignore(o => o.DomainEvents);
        }
    }
}
