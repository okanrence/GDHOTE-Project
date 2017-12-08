using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("vx_HUB_RoleSubMenus")]
   public class RoleSubMenuView : SubMenu
    {
        public string RoleMenuId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleMenuStatus { get; set; }
        public DateTime RoleMenuDate { get; set; }
    }
}
