using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class SexService : BaseService
    {
        public static IEnumerable<Sex> GetSex()
        {
            var sexs = new List<Sex>
            {
                new Sex {Id = 1, Code = "F", Description = "Female"},
                new Sex {Id = 2, Code = "M", Description = "Male"}
            };
            return sexs;
        }
    }
}
