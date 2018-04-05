using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_Users")]
    public class UserViewModel : User
    {
        public int RoleTypeId { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
