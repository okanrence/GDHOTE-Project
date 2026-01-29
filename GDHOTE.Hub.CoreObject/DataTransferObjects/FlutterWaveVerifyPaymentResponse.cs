using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class FlutterWaveVerifyPaymentResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public PaymentData data { get; set; }
        public string details { get; set; }
    }

    public class PaymentData
    {
        public string flwref { get; set; }
        public string txid { get; set; }
    }
}
