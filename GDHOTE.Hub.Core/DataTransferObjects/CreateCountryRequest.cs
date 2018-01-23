using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.DataTransferObjects
{
    [TableName("HUB_Countries")]
    public class CreateCountry
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
