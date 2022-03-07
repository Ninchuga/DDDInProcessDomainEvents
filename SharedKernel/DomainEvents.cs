using System;
using System.Threading.Tasks;

namespace SharedKernel
{
    public class DomainEvents
	{
		public static Func<Type, object> Handler;

		public static async Task Dispatch(IDomainEvent domainEvent)
		{
			var eventType = domainEvent.GetType();
			var handlerType = typeof(IHandler<>).MakeGenericType(eventType);
			dynamic handler = Handler.Invoke(handlerType);
			await handler.Handle((dynamic)domainEvent);
		}
	}
}
