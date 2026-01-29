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
        public int RoleId { get; set; }
        [Required]
        [Display(Name = "Sub Menu")]
        public int SubMenuId { get; set; }
    }
}
