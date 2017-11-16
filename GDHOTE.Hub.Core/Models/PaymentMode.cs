using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_ModeOfPayments")]
    [PrimaryKey("PaymentModeId")]
    public class PaymentMode
    {
        public int PaymentModeId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
