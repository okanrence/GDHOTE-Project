using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_YearGroup")]
    public class YearGroup
    {
        public int Id { get; set; }
        public string YearGroupCode { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
