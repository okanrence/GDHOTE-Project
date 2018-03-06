using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
  public   class PaymentModeFormViewModel
    {
        public PaymentMode PaymentMode { get; set; }
        public List<Status> Status { get; set; }
    }
}
