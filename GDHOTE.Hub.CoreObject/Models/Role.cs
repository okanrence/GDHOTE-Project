using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Roles")]
    [PrimaryKey("RoleId", AutoIncrement = false)]
    public class Role : BaseModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public int StatusId { get; set; }
      
    }
}