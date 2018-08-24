using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class VerifyPaymentRequest
    {
        public string PaymentReference { get; set; }
        public string MerchantReference { get; set; }
        public int GatewayId { get; set; }
    }
}
