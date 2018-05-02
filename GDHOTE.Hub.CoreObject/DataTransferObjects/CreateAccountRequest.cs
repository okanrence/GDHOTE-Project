using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateAccountRequest
    {
        public string AccountName { get; set; }
        public long MemberId { get; set; }
    }
}
