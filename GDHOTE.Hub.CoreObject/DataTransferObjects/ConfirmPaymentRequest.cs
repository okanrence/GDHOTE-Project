﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class ConfirmPaymentRequest
    {
        [Required]
        public string PaymentReference { get; set; }
        [Required]
        public string Action { get; set; }
        public string Comment { get; set; }
    }
}
