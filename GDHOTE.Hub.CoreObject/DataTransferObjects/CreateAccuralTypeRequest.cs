using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateAccuralTypeRequest
    {
        [Required(ErrorMessage = "Please specify Accural Type")]
        [Display(Name = "Accural Type")]
        public string Name { get; set; }
        public int Period { get; set; }
    }
}
