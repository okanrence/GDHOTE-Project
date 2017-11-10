using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Dtos;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_States")]
    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [Required]
        [Display(Name = "State Name")]
        public string StateName { get; set; }
        public DateTime RecordDate { get; set; }
        //public CountryDto Country { get; set; }
    }
}
