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

        public async Task SaveChanges<T>(AggregateRoot<T> aggregateRoot, CancellationToken cancellationToken = default)
        {
            bool successfullySavedToDb = SuccessfullySavedBy(await base.SaveChangesAsync(cancellationToken));
            if (!successfullySavedToDb)
                throw new Exception($"No rows were inserted for the given aggregate {aggregateRoot.GetType().Name}");

            // In here dispatch only events that are used by other aggregates inside of executing bounded context
            // Or insert it in database and than this event will be picked up by a worker and published to different context or message broker
            if(successfullySavedToDb && aggregateRoot.DomainEvents.Any())
            {
                foreach (var domainEvent in aggregateRoot.DomainEvents)
                {
                    DomainEvents.Dispatch(domainEvent);
                }

                aggregateRoot.ClearEvents();
            }
        }

        private bool SuccessfullySavedBy(int insertedEntitiesNumber) => insertedEntitiesNumber > 0;
    }
}
