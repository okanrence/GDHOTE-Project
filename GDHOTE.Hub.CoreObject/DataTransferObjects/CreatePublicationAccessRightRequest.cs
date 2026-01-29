using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePublicationAccessRightRequest
    {
        [Required(ErrorMessage = "Please specify Access Right")]
        [Display(Name = "Access Right")]
        public string Name { get; set; }
    }
}
