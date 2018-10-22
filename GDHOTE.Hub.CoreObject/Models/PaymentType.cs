﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_PaymentTypes")]
    public class PaymentType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long AccountId { get; set; }
        public int StatusId { get; set; }
    }
}
