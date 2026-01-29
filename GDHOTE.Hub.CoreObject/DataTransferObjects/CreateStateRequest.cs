using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateStateRequest
    {
        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Please specify State")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
