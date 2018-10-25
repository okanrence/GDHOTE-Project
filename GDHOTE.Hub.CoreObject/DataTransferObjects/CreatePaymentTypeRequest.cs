﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePaymentTypeRequest
    {
        [Required(ErrorMessage = "Please specify Payment Type")]
        [Display(Name = "Payment Type")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please specify account")]
        [Display(Name = "Internal Account")]
        public long AccountId { get; set; }
    }
}