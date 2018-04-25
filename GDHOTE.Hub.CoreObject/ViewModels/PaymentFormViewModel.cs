using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class PaymentFormViewModel : CreatePaymentRequest
    {
        public List<PaymentTypeResponse> PaymentTypes { get; set; }
        public List<PaymentModeResponse> ModeOfPayments { get; set; }
        public List<CurrencyResponse> Currencies { get; set; }
    }
}
