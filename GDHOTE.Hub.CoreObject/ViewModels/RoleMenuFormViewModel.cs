using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class RoleMenuFormViewModel
    {
        public RoleMenu RoleMenu { get; set; }
        public List<MainMenu> MainMenus { get; set; }
        public List<SubMenu> SubMenus { get; set; }
        public List<Role> Roles { get; set; }
        public List<Status> Statuses { get; set; }
    }
}
