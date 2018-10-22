using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Activities")]
    public class Activity : BaseModel
    {
        public long Id { get; set; }
        public long ActivityTypeId { get; set; }
        public long MemberId { get; set; }
        public int StatusId { get; set; }
        public string ActivityKey { get; set; }
        public string Remarks { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
