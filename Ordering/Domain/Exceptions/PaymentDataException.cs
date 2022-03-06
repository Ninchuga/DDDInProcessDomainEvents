using System;

namespace Ordering.Domain.Exceptions
{
    public class PaymentDataException : Exception
    {
        public PaymentDataException()
        { }

        public PaymentDataException(string message)
            : base(message)
        { }

        public PaymentDataException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
