using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class ApproveMemberRequest
    {
        [Required]
        public long MemberId { get; set; }
        [Required]
        public string Action { get; set; }
        public string Sex { get; set; }
        public string Comment { get; set; }
    }
}
