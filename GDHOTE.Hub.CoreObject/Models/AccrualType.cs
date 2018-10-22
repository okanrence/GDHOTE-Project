using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_AccrualTypes")]
    public class AccrualType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
    }
}
