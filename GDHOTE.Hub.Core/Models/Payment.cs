using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_Payments")]
    [PrimaryKey("PaymentId")]

    public class Payment
    {
        public int PaymentId { get; set; }
        public int MemberKey { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "Payment Type")]
        public int PaymentTypeId { get; set; }
        public string Narration { get; set; }
        public int CreatedBy { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
