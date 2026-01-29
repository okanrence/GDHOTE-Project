using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class PaymentFormViewModel : CreatePaymentRequest
    {
        public List<PaymentTypeResponse> PaymentTypes { get; set; }
        public List<PaymentModeResponse> ModeOfPayments { get; set; }
        public List<CurrencyResponse> Currencies { get; set; }
    }
}
