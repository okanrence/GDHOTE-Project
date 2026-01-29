using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class MailPublicationRequest
    {
        [Required(ErrorMessage = "Please specify Publication")]
        [Display(Name = "Publication")]
        public long PublicationId { get; set; }
        public long MemberId { get; set; }
    }
}
