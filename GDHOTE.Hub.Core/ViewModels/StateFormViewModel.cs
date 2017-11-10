using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Dtos;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.ViewModels
{
    public class StateFormViewModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public State State { get; set; }
    }
}
