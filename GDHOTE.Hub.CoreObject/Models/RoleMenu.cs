using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_RoleMenus")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class RoleMenu : BaseModel
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string SubMenuId { get; set; }
        public int StatusId { get; set; }
    }
}
