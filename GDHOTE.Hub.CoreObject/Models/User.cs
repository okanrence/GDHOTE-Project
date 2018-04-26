using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Users")]
    public class User : BaseModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int UserStatusId { get; set; }
        public string RoleId { get; set; }
        public int ChannelId { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool PasswordChange { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
    }
}
