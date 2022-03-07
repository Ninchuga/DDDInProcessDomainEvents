using Microsoft.EntityFrameworkCore;
using Payment.Infrastructure.EntityConfiguration;
using SharedKernel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Persistence
{
    public class PaymentContext : DbContext
    {
        private const string ConnectionString = @"Server=.\SQLEXPRESS;Initial Catalog=ShoppingDb;Integrated Security=SSPI;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payment");
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }

        public DbSet<Domain.Entities.Payment> Payments { get; set; }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            bool successfullySavedToDb = SuccessfullySavedBy(await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false));
            if (!successfullySavedToDb)
                throw new Exception("No rows were affected.");

            // In here dispatch only events that are used by other aggregates inside of executing bounded context
            // Or insert it in database and than this event will be picked up by a worker and published to different context or message broker
            await DispatchDomainEvents(this);
        }

        public async Task DispatchDomainEvents(PaymentContext ctx)
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
