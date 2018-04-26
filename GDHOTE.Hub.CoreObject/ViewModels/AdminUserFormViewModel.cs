using System.Collections.Generic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class AdminUserFormViewModel : CreateAdminUserRequest
    {
        public List<RoleTypeResponse> RoleTypes { get; set; }
        public List<RoleResponse> Roles { get; set; }
        public List<UserStatus> UserStatuses { get; set; }
    }
}
