using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Checkers")]
    public class Checker
    {
        public int Id { get; set; }
        public string ApplicationId { get; set; }
        public DateTime CheckDate { get; set; }
        public DateTime? LastCheckDate { get; set; }
        public int StatusId { get; set; }
    }
}
