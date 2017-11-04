using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class PaymentModeService : BaseService
    {
        public static IEnumerable<PaymentMode> GetPaymentModes()
        {
            var paymentModes = new List<PaymentMode>
            {
                new PaymentMode {Id = 1, Code = "C", Description = "Cash"},
                new PaymentMode {Id = 2, Code = "T", Description = "Bank Transfer"},
                new PaymentMode {Id = 3, Code = "P", Description = "POS"},
                new PaymentMode {Id = 4, Code = "W", Description = "Web"}
            };
            return paymentModes;
        }
    }
}
