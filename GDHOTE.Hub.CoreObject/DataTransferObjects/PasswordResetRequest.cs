using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class PasswordResetRequest
    {
        [Required(ErrorMessage = "Please specify your Username")]
        public string Username { get; set; }
        public int ChannelId { get; set; }
    }
}
