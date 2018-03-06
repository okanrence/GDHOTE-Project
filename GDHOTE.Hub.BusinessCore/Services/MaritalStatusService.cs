using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MaritalStatusService
    {
        public static List<MaritalStatus> GetMaritalStatuses()
        {
            var maritalStatuses = new List<MaritalStatus>
            {
                new MaritalStatus {Code = "S", Description = "Single"},
                new MaritalStatus {Code = "M", Description = "Married"},
                new MaritalStatus {Code = "D", Description = "Divorced"},
                new MaritalStatus {Code = "K", Description = "Seperated"}
            };
            return maritalStatuses;
        }
    }
}
