using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class SmsMessageRequest
    {
        //public long MemberId { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public string MobileNumber { get; set; }
    }
}
