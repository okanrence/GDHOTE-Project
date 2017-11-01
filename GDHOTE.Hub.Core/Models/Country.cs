using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_Countries")]
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        public string Status { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
