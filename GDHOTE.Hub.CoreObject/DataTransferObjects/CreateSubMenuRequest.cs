using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateSubMenuRequest
    {
        [Required(ErrorMessage = "Please specify Main Menu")]
        [Display(Name = "Main Menu")]
        public string MenuId { get; set; }
        [Required(ErrorMessage = "Please specify Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please specify Url")]
        [Display(Name = "Url")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Please specify Display Sequence")]
        [Display(Name = "Display Sequence")]
        public int DisplaySequence { get; set; }
    }
}
