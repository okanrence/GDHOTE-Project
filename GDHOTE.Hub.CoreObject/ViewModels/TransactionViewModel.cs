using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class TransactionViewModel : Transaction
    {
        public string TransactionStatus { get; set; }
        public string CreatedBy { get; set; }
    }
}
