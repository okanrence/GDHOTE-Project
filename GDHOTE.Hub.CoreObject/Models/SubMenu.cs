using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_SubMenus")]
    [PrimaryKey("SubMenuId", AutoIncrement = false)]
    public class SubMenu
    {
        public string SubMenuId { get; set; }
        [Required]
        [Display(Name = "Main Menu")]
        public string MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string DisplaySequence { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
