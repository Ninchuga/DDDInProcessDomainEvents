using Microsoft.Extensions.DependencyInjection;
using Payment.Application.EventHandlers;
using Payment.Domain.Events;
using Payment.Domain.Services;
using Payment.Infrastructure.Persistence;
using Payment.Infrastructure.Repository;
using Payment.Infrastructure.Services;
using SharedKernel;
using SharedKernel.IntegrationEvents;

namespace Payment
{
    public static class DependencyInjection
    {
        public static ServiceCollection AddPaymentServices(this ServiceCollection services)
        {
            services.AddScoped<IHandler<OrderPlacedIntegrationEvent>, OrderPlacedIntegrationEventHandler>();
            services.AddScoped<IHandler<PaymentAddedEvent>, PaymentAddedEventHandler>();
            services.AddScoped<PaymentContext>();
            services.AddTransient<PaymentRepository>();
            services.AddTransient<PaymentService>();
            services.AddTransient<PaymentGateway>();

            return services;
        }
    }
}
