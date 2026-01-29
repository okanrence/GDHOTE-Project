using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class UploadMemberRequest
    {
        public int ChannelCode { get; set; }
        [Required(ErrorMessage = "Please specify Upload File")]
        [Display(Name = "Upload File")]
        public string UploadFile { get; set; }
        public byte[] UploadFileContent { get; set; }
    }
}
