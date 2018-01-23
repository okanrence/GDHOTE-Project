using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_States")]
    [PrimaryKey("StateId")]
    public class State
    {
        public int StateId { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "State Name")]
        public string StateName { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
