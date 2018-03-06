using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    public class YearGroupFormViewModel
    {
        public YearGroup YearGroup { get; set; }
        public List<Status> Status { get; set; }
    }
}
