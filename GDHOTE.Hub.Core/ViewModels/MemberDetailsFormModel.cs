using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
  public  class MemberDetailsFormModel
    {
        public MemberDetails MemberDetails { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<YearGroup> YearGroups { get; set; }

    }
}
