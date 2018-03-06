
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_UserRoles")]
    [PrimaryKey("UserRoleId")]
    public class UserRoles
    {
        public int UserRoleId { get; set; }

        public string UserId { get; set; }

        public int RoleId { get; set; }
    }
}
