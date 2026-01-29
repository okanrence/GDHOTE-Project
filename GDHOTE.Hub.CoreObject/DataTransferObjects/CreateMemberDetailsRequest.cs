using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateMemberDetailsRequest
    {
        public long MemberId { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Alternative Number")]
        public string AlternateNumber { get; set; }
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailAddress { get; set; }
        [Display(Name = "State Of Origin")]
        public int StateOfOriginId { get; set; }
        [Display(Name = "Country Of Origin")]
        public int CountryOfOriginId { get; set; }
        [Display(Name = "Residence State")]
        [Required]
        public int ResidenceStateId { get; set; }
        [Required]
        [Display(Name = "Residence Country")]
        public int ResidenceCountryId { get; set; }
        [Required]
        [Display(Name = "Residence Address")]
        public string ResidenceAddress { get; set; }
        [Display(Name = "Date Wedded")]
        public DateTime? DateWedded { get; set; }
        [Display(Name = "Year Group")]
        public int YearGroupId { get; set; }
        [Display(Name = "Highest Degree Obtained")]
        public string HighestDegreeObtained { get; set; }
        [Display(Name = "Current Place Of Work/School")]
        public string CurrentWorkPlace { get; set; }
    }
}
