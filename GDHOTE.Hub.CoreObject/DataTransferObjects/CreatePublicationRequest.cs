using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
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
        [Required(ErrorMessage = "Please specify Upload File")]
        [Display(Name = "Upload File")]
        public string UploadFile { get; set; }
        [Display(Name = "Cover Page Image")]
        public string CoverPageImage { get; set; }
        public DateTime DatePublished { get; set; }
        public byte[] UploadFileContent { get; set; }
    }
}
