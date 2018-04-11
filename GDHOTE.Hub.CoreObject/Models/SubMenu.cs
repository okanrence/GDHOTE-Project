using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_SubMenus")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class SubMenu : BaseModel
    {
        public string Id { get; set; }
        public string MenuId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int StatusId { get; set; }
        public int DisplaySequence { get; set; }
    }
}
