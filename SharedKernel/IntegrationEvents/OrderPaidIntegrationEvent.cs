using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.IntegrationEvents
{
    public class OrderPaidIntegrationEvent : IDomainEvent
    {
        public Guid OrderId { get; set; }
        public string UserName { get; set; }
    }
}
