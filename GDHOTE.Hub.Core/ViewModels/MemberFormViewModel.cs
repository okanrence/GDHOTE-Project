using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
   public class MemberFormViewModel
    {
        public IEnumerable<Sex> Sexs { get; set; }
        public IEnumerable<MaritalStatus> MaritalStatus { get; set; }
        public Member Member { get; set; }
    }
}
