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
    [PrimaryKey("UserId", AutoIncrement = false)]
    public class User : BaseModel
    {
        public string UserId { get; set; }
        [DisplayName("Email Address")]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DisplayName("User Status")]
        public int UserStatusId { get; set; }
        [Required]
        [DisplayName("Role")]
        public string RoleId { get; set; }
        public int ChannelId { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool PasswordChange { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
    }
}
