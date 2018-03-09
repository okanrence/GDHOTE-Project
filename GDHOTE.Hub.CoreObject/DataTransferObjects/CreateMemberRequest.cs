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
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please specify Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please specify Gender")]
        public string Gender { get; set; }
        public bool MagusFlag { get; set; }
        public bool InitiationFlag { get; set; }
        public string MaritalStatus { get; set; }
        //[Required(ErrorMessage = "Please specify a mobile number")]
        //[RegularExpression("^[0-9]{6,14}$", ErrorMessage = "Please enter a valid mobile number")]
        //[Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please specify date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public DateTime? MagusDate { get; set; }          

    }
}