using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;
using SharedKernel;
using Ordering.Application.Services;
using Ordering.Infrastructure.Repositories;
using Ordering.Application.EventHandlers;
using Ordering.Domain.Events;
using SharedKernel.IntegrationEvents;
using Ordering.Infrastructure.Mail;

namespace Ordering
{
    public static class DependencyInjection
    {
        public static ServiceCollection AddOrderingServices(this ServiceCollection services)
        {
            services.AddScoped<IHandler<OrderAddedEvent>, OrderAddedEventHandler>();
            services.AddScoped<IHandler<OrderPaidIntegrationEvent>, OrderPaidEventHandler>();
            services.AddScoped<OrderContext>();
            services.AddTransient<OrderingService>();
            services.AddTransient<OrderRepository>();
            services.AddTransient<EmailService>();

            return services;
        }
    }
}
