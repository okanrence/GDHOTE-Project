using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_PaymentGatewayDetails")]
    public class PaymentGatewayDetail
    {
        public long Id { get; set; }
        public int GatewayId { get; set; }
        public string PaymentReference { get; set; }
        public string MerchantReference { get; set; }
        public string GatewayResponseCode { get; set; }
        public string GatewayResponseMessage { get; set; }
        public string GatewayResponseDetails { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
