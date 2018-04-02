using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePublicationRequest
    {
        [Required(ErrorMessage = "Please specify Title")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please specify Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please specify Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please specify Access Right")]
        [Display(Name = "Access Right")]
        public int AccessRightId { get; set; }
        [Display(Name = "Upload File")]
        public string UploadFile { get; set; }
    }
}
