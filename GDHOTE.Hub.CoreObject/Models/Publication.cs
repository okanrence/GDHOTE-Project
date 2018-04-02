using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Publications")]
    public class Publication : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AccessRightId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public string UploadFile { get; set; }
    }
}
