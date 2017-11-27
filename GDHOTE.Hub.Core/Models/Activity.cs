using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_Activities")]
    [PrimaryKey("ActivityId")]
    public class Activity
    {
        public int ActivityId { get; set; }
        public int ActivityTypeId { get; set; }
        public int MemberKey { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }

    }
}
