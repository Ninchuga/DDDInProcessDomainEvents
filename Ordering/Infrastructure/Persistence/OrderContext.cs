using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entitites;
using Ordering.Infrastructure.EntityConfigurations;
using SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        private const string ConnectionString = @"Server=.\SQLEXPRESS;Initial Catalog=ShoppingDb;Integrated Security=SSPI;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ordering");
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            bool successfullySavedToDb = SuccessfullySavedBy(await base.SaveChangesAsync(cancellationToken));
            if (!successfullySavedToDb)
                throw new Exception("No rows were affected.");

            // In here dispatch only events that are used by other aggregates inside of executing bounded context; let the event handlers dispatch integration events
            // Or insert it in database and than this event will be picked up by a worker and published to different context or message broker
            // To Archive transactional behavior inside this context, dispatch domain events before saving the changes/commiting.
            await DispatchDomainEvents(this);
        }

        public async Task DispatchDomainEvents(OrderContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            foreach (var domainEvent in domainEvents)
                await DomainEvents.Dispatch(domainEvent);
        }

        private bool SuccessfullySavedBy(int insertedEntitiesNumber) => insertedEntitiesNumber > 0;
    }
}
