using System;
using System.Collections.Generic;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("")]
    public class PaymentTypeViewModel : PaymentType
    {
        public string Status   { get; set; }
        public string CreatedBy { get; set; }
    }
}
