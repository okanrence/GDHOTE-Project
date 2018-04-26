using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class RoleMenuFormViewModel : CreateRoleMenuRequest
    {
        public List<MainMenuResponse> MainMenus { get; set; }
        public List<SubMenuResponse> SubMenus { get; set; }
        public List<RoleResponse> Roles { get; set; }
        public List<Status> Statuses { get; set; }
    }
}
