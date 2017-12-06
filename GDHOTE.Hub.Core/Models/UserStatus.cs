
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_UserStatus")]
    [PrimaryKey("UserStatusId")]
    public class UserStatus
    {
        public int UserStatusId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
