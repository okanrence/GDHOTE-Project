using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class RoleFormViewModel : CreateRoleRequest
    {
        public List<Status> Statuses { get; set; }
        public List<RoleTypeResponse> RoleTypes { get; set; }
    }
}
