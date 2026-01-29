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
        [Required(ErrorMessage = "Please specify Country")]
        [Display(Name = "Country")]
        public string Name { get; set; }
    }
}
