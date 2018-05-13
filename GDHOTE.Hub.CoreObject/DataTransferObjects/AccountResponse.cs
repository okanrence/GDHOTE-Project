using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class AccountResponse
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
    }
}
