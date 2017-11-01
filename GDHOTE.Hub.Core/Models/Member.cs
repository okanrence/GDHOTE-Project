using System;
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
        [Required]
        [Display(Name = "Member Code")]
        public string MemberCode { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string MagusFlag { get; set; }
        public string MartialStatus { get; set; }
        public string StatusCode { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public DateTime MagusDate { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
