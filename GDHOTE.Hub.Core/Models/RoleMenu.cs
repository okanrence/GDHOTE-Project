using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_RoleMenus")]
    [PrimaryKey("RoleMenuId", AutoIncrement = false)]
    public class RoleMenu
    {
        public string RoleMenuId { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Ignore]
        //[Required]
        [Display(Name = "Main Menu")]
        public string MenuId { get; set; }
        [Required]
        [Display(Name = "Sub Menu")]
        public string SubMenuId { get; set; }
        [Required]
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
