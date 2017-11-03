using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class MaritalStatusService
    {
        public static IEnumerable<MaritalStatus> GetMaritalStatuses()
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
