using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{

    [TableName("HUB_Roles")]
    [PrimaryKey("RoleId", AutoIncrement = false)]
    public class Role
    {
        public string RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}