using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class AdminUserFormViewModel : CreateAdminUserRequest
    {
        public List<RoleType> RoleTypes { get; set; }
        public List<Role> Roles { get; set; }
        public List<UserStatus> UserStatuses { get; set; }
    }
}
