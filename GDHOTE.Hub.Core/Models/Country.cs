using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_Countries")]
    [PrimaryKey("CountryKey")]
    public class Country
    {
        public string CountryKey { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
