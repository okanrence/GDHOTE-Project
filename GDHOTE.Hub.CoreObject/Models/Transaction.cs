using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Transactions")]
    public class Transaction : BaseModel
    {
        public long Id { get; set; }
        public string TransactionReference { get; set; }
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
        public string DebitCredit { get; set; }
        public string Narration { get; set; }
        public int TransactionStatusId { get; set; }
        public int CurrencyId { get; set; }
        public int PaymentModeId { get; set; }
        public long PaymentId { get; set; }
        public string BankReference { get; set; }
        public string Remarks { get; set; }
        public long ApprovedById { get; set; }
        public DateTime? DateApproved { get; set; }
    }
}
