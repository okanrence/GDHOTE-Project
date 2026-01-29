using System;
namespace GDHOTE.Hub.CoreObject.Models
{
    public class BaseModel
    {
        public long CreatedById { get; set; }
        public long UpdatedById { get; set; }
        public long DeletedById { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? RecordDate { get; set; }
    }
}
