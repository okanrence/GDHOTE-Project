using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class FlutterWaveVerifyPaymentRequest
    {
        public string txref { get; set; }
        public string SECKEY { get; set; }
    }
}
