using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateServiceLogRequest
    {
        public int ServiceId { get; set; }
        public int RecordCount { get; set; }
    }
}
