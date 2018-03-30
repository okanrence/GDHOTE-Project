using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateRoleRequest
    {
        [Required(ErrorMessage = "Please specify Role")]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
