using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Statuses")]
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
