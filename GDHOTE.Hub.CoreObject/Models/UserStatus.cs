
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_UserStatuses")]
    public class UserStatus : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
