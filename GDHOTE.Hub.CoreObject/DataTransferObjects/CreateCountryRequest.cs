using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateCountryRequest
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
