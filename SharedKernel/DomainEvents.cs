using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
	public class DomainEvents
	{
		private static List<Type> _handlers;

		public static void Init(Assembly executingAssembly)
		{
			if(_handlers == null)
            {
				_handlers = executingAssembly
				.GetTypes()
				.Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
				.ToList();
			}
            else
            {
				var handlers = executingAssembly
				.GetTypes()
				.Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)));

				_handlers.AddRange(handlers);
			}
		}

		public static void Dispatch(IDomainEvent domainEvent)
		{
			foreach (Type handlerType in _handlers)
			{
				bool canHandleEvent = handlerType.GetInterfaces()
					.Any(x => x.IsGenericType
							  && x.GetGenericTypeDefinition() == typeof(IHandler<>)
							  && x.GenericTypeArguments[0] == domainEvent.GetType());

				if (canHandleEvent)
				{
					dynamic handler = Activator.CreateInstance(handlerType);
					handler.Handle((dynamic)domainEvent);
				}
			}
		}

		public static void Dispatch(IEnumerable<IDomainEvent> domainEvents)
		{
            foreach (var domainEvent in domainEvents)
            {
				foreach (Type handlerType in _handlers)
				{
					bool canHandleEvent = handlerType.GetInterfaces()
						.Any(x => x.IsGenericType
								  && x.GetGenericTypeDefinition() == typeof(IHandler<>)
								  && x.GenericTypeArguments[0] == domainEvent.GetType());

					if (canHandleEvent)
					{
						dynamic handler = Activator.CreateInstance(handlerType);
						handler.Handle((dynamic)domainEvent);
					}
				}
			}
		}
	}
}
