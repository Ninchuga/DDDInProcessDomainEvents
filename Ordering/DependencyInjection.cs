using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;
using SharedKernel;
using System.Reflection;
using Ordering.Application.Services;
using Ordering.Infrastructure.Repositories;

namespace Ordering
{
    public static class DependencyInjection
    {
        public static ServiceCollection AddOrderingServices(this ServiceCollection services)
        {
            DomainEvents.Init(Assembly.GetExecutingAssembly());
            services.AddScoped<OrderContext>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<OrderingService>();
            services.AddTransient<OrderRepository>();

            return services;
        }
    }
}
