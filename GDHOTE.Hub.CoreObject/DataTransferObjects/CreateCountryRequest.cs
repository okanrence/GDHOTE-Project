using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateCountryRequest
    {
        [Required]
        public string CountryCode { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Name { get; set; }
    }
}
