﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class PaymentFormViewModel
    {
        public Payment Payment { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
        public List<PaymentMode> ModeOfPayments { get; set; }
    }
}