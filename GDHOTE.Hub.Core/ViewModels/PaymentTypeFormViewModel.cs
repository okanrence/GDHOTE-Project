using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class PaymentTypeFormViewModel
    {
        public PaymentType PaymentType { get; set; }
        public List<Status> Status { get; set; }
    }
}
