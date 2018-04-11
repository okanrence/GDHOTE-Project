using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_RoleMenus")]
    public class RoleMenuViewModel : RoleMenu
    {
        public string SubMenu { get; set; }
        public string SubMenuUrl { get; set; }
        public int DisplaySequence { get; set; }
        public string Status { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
    }
}
