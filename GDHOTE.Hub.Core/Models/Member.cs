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
        public string Surname { get; set; }
        [Required]
        public string Gender { get; set; }
        public bool InitiationFlag { get; set; }
        public bool MagusFlag { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        public string StatusCode { get; set; }
        public int ChannelCode { get; set; }
        public string DeleteFlag { get; set; }
        public string ApprovedFlag { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public DateTime? MagusDate { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int OfficerId { get; set; }
        public DateTime OfficerDate { get; set; }

    }
}
