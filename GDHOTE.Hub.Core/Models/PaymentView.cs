using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("vx_HUB_Payments")]
    public class PaymentView : Payment
    {
        public string MemberCode { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMode { get; set; }
        public string Currency { get; set; }
      
    }
}
