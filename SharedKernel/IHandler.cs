using System.Threading.Tasks;

namespace SharedKernel
{
    public interface IHandler<in T> where T : IDomainEvent
	{
		Task Handle(T domainEvent);
	}
}
