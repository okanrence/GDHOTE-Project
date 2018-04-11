using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateRoleMenuRequest
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Required]
        [Display(Name = "Sub Menu")]
        public string SubMenuId { get; set; }
    }
}
