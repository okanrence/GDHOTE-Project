using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateAccountTypeRequest
    {
        [Required(ErrorMessage = "Please specify Account Type")]
        [Display(Name = "Account Type")]
        public string Name { get; set; }
    }
}
