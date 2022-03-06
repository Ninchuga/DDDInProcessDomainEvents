using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment.Application.EventHandlers;
using Payment.Infrastructure.Persistence;
using Payment.Infrastructure.Repository;
using SharedKernel;
using SharedKernel.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public static class DependencyInjection
    {
        public static ServiceCollection AddPaymentServices(this ServiceCollection services)
        {
            DomainEvents.Init(Assembly.GetExecutingAssembly());
            services.AddScoped<PaymentContext>();
            services.AddTransient<PaymentRepository>();

            return services;
        }
    }
}
