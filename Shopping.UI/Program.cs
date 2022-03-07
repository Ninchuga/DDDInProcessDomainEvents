using Microsoft.Extensions.DependencyInjection;
using Ordering;
using Payment;
using SharedKernel;
using System;
using System.Windows.Forms;

namespace Shopping.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<Form1>();
            services.AddOrderingServices();
            services.AddPaymentServices();

            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
            DomainEvents.Handler = (type) => ServiceLocator.Current.GetInstance(type);
        }
    }
}
