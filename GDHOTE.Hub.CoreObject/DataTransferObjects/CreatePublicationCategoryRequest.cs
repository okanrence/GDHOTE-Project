﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreatePublicationCategoryRequest
    {
        [Required(ErrorMessage = "Please specify Category")]
        [Display(Name = "Category")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please specify Display Image")]
        [Display(Name = "Display Image")]
        public string DisplayImageFile { get; set; }
        public byte[] DisplayImageFileContent { get; set; }
    }
}