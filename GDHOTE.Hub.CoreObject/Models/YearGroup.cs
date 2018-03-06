using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_YearGroup")]
    public class YearGroup
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Year Code")]
        public string YearGroupCode { get; set; }
        [Required]
        [Display(Name = "Year Description")]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
