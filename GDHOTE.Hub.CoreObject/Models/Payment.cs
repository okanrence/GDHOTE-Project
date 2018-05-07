using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Payments")]
    public class Payment : BaseModel
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentTypeId { get; set; }
        public int PaymentModeId { get; set; }
        public int CurrencyId { get; set; }
        public string Narration { get; set; }
        public int PaymentStatusId { get; set; }
        public string Remarks { get; set; }
        public string PaymentReference { get; set; }
        public DateTime? DateApproved { get; set; }
    }
}
