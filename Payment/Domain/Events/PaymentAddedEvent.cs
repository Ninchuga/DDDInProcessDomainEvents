using SharedKernel;

namespace Payment.Domain.Events
{
    public class PaymentAddedEvent : IDomainEvent
    {
        public PaymentAddedEvent(Entities.Payment payment)
        {
            Payment = payment;
        }

        public Entities.Payment Payment { get; }
    }
}
