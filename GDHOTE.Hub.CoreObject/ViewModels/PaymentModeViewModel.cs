﻿using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_ModeOfPayments")]
    public class PaymentModeViewModel : PaymentMode
    {
        public string Status { get; set; }
    }
}