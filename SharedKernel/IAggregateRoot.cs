using System.Collections.Generic;

namespace SharedKernel
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void ClearEvents();
    }
}
