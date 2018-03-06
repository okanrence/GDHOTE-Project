using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("vx_HUB_SubMenus")]
    public class SubMenuView : SubMenu
    {
        public string MenuName { get; set; }

    }
}
