﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{

    [TableName("HUB_Roles")]
    [PrimaryKey("PaymentTypeId")]
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}