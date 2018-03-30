﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateMemberRequest
    {
        [Required(ErrorMessage = "Please specify First name")]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please specify Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please specify Gender")]
        public string Gender { get; set; }
        public bool MagusFlag { get; set; }
        [Display(Name = "Initiation")]
        public bool InitiationFlag { get; set; }
        [Required(ErrorMessage = "Please specify Martial Status")]
        [Display(Name = "Martial Status")]
        public string MaritalStatus { get; set; }
        public string MobileNumber { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please specify date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public DateTime? MagusDate { get; set; }

    }
}
