namespace Payment.Infrastructure.Services
{
    public class PaymentGateway
    {
        public bool ChargeAmout(decimal amount)
        {
            // Call third party payment provider and charge provided amount
            // Return Success or False result

            return true;
        }
    }
}
