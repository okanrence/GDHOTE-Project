using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class SendEmailRequest
    {
        public long MemberId { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string RecipientEmailAddress { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
    }
}
