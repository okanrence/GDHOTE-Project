using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.Core.DataTransferObjects
{
    public class Response
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Reference { get; set; }
    }
}
