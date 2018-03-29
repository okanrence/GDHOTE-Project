using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Currencies")]
    public class Currency : BaseModel
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
    }
}
