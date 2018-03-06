using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class GenderService : BaseService
    {
        public static List<Gender> GetGenders()
        {
            var genders = new List<Gender>
            {
                new Gender {Id = 1, Code = "F", Description = "Female"},
                new Gender {Id = 2, Code = "M", Description = "Male"}
            };
            return genders;
        }
    }
}
