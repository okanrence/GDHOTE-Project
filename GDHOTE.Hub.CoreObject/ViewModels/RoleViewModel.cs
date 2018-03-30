using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_Roles")]
    public class RoleViewModel : Role
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
