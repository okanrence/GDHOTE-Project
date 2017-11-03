﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_Members")]
    [PrimaryKey("MemberKey")]
    public class Member
    {
        public int MemberKey { get; set; }
        [Display(Name = "Member Code")]
        public string MemberCode { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Sex { get; set; }
        public bool MagusFlag { get; set; }
        [Required]
        public string MartialStatus { get; set; }
        public string StatusCode { get; set; }
        public string DeleteFlag { get; set; }
        public string ApprovedFlag { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public DateTime? MagusDate { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
