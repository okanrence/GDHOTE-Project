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
    public class RoleMenu : BaseModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int SubMenuId { get; set; }
        public int StatusId { get; set; }
        public string RoleMenuKey { get; set; }
    }
}
