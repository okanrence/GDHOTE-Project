using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Currencies")]
    [PrimaryKey("CurrencyId")]
    public class Currency
    {
        public int CurrencyId { get; set; }
        [Required]
        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }
        [Required]
        [Display(Name = "Currency Description")]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
