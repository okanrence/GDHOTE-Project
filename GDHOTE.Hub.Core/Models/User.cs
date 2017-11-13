using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_Users")]
    [PrimaryKey("UserId")]
    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        public int UserStatusId { get; set; }
        public int RoleId { get; set; }
        [Ignore]
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
