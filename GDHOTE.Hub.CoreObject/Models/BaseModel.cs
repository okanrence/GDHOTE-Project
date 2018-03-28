using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.Models
{
    public class BaseModel
    {
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public string DeletedById { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}
