using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.Models
{
    public class BaseModel
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}
