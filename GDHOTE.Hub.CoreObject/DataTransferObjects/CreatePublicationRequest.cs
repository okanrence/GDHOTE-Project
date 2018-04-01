using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
   public class CreatePublicationRequest
    {
        [Required(ErrorMessage = "Please specify Publication")]
        [Display(Name = "Publication")]
        public string Name { get; set; }
    }
}
