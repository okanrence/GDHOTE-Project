using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Accounts")]
    public class Account : BaseModel
    {
        public long Id { get; set; }
        public string AccountName { get; set; }
        public long MemberId { get; set; }
        public decimal Balance { get; set; }
        public int StatusId { get; set; }
    }
}
