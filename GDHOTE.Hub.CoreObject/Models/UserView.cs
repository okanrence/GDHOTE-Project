using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("vx_HUB_Users")]
    public class UserView : User
    {
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
