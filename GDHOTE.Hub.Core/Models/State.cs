using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_States")]
    [PrimaryKey("StateKey")]
    public class State
    {
        public int StateKey { get; set; }
        public int CountryKey { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}
