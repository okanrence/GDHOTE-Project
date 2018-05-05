﻿using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_AccountTypes")]
    public class AccountType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
    }
}