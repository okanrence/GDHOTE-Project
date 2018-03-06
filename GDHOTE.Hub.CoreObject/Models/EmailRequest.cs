using GDHOTE.Hub.CoreObject.Enumerables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CoreObject.Models
{
    public class EmailRequest
    {
        public string RecipientEmailAddress { get; set; }
        public string Subject { get; set; }
        public EmailType Type { get; set; }
        public Hashtable Data { get; set; }

    }
}
