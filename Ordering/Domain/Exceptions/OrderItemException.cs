using System;

namespace Ordering.Domain.Exceptions
{
    public class OrderItemException : Exception
    {
        public OrderItemException()
        { }

        public OrderItemException(string message)
            : base(message)
        { }

        public OrderItemException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
