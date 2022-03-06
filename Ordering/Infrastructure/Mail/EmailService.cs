using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService
    {
        public void SendOrderPlacedEmail(Guid orderId, decimal orderTotalPrice, string userEmail)
        {
            // send email to user that order with #Id has been placed
        }
    }
}
