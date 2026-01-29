using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateMainMenuRequest
    {
        [Required(ErrorMessage = "Please specify Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please specify Display Sequence")]
        [Display(Name = "Display Sequence")]
        public int DisplaySequence { get; set; }
    }
}
