using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Notifications")]
    public class Notification : BaseModel
    {
        public long Id { get; set; }
        public string Recipient { get; set; }
        public int NotificationTypeId { get; set; }
        public string ContentBody { get; set; }
        public char Status { get; set; }
    }
}
