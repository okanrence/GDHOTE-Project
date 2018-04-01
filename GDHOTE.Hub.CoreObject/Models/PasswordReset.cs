using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_PasswordResets")]
    public class PasswordReset
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string ResetCode { get; set; }
        public int ChannelId { get; set; }
        public int UserType { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateExpiry { get; set; }
        public DateTime? DateUpdated { get; set; }

    }
}
