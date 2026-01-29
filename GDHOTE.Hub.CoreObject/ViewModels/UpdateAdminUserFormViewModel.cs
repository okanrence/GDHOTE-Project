using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class UpdateAdminUserFormViewModel : UpdateAdminUserRequest
    {
        public List<RoleTypeResponse> RoleTypes { get; set; }
        public List<RoleResponse> Roles { get; set; }
        public List<UserStatusResponse> UserStatuses { get; set; }
    }
}
