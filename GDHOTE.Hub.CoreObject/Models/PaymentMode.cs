﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_ModeOfPayments")]
    public class PaymentMode : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
    }
}
