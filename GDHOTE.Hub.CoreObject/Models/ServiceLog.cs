using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_ServiceLogs")]
    public class ServiceLog
    {
        public long Id { get; set; }
        public int ServiceId { get; set; }
        public int RecordCount { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
