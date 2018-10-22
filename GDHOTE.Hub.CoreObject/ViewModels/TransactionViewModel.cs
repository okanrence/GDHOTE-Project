using System;
using System.Collections.Generic;
using System.Text;
using NPoco;
using Transaction = GDHOTE.Hub.CoreObject.Models.Transaction;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_Transactions")]
    public class TransactionViewModel : Transaction
    {
        public string AccountName { get; set; }
        public string TransactionStatus { get; set; }
        public string CreatedBy { get; set; }
        public string PaymentMode { get; set; }
        public string Currency { get; set; }
    }
}
