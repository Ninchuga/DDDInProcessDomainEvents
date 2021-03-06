using System.Collections.Generic;

namespace SharedKernel
{
    public abstract class AggregateRoot<T> : BaseEntity<T>, IAggregateRoot
    {
        public AggregateRoot(T id) : base(id)
        {
        }

		private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
		public virtual IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

		protected virtual void AddDomainEvent(IDomainEvent newEvent)
		{
			_domainEvents.Add(newEvent);
		}

		public virtual void ClearEvents()
		{
			_domainEvents.Clear();
		}
	}
}
