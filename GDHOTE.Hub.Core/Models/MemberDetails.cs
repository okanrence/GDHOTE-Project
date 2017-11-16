using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_MemberDetails")]
    [PrimaryKey("MemberDetailsId")]
    public class MemberDetails
    {
        public int MemberDetailsId { get; set; }
        public int MemberKey { get; set; }
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Alternative Number")]
        public string AlternateNumber { get; set; }
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string EmailAddress { get; set; }
        [Display(Name = "State Of Origin")]
        public string StateOfOriginCode { get; set; }
        [Display(Name = "Country Of Origin")]
        public string CountryOfOriginCode { get; set; }
        [Display(Name = "Residence State")]
        [Required]
        public string ResidenceStateCode { get; set; }
        [Required]
        [Display(Name = "Residence Country")]
        public string ResidenceCountryCode { get; set; }
        [Required]
        [Display(Name = "Residence Address")]
        public string ResidenceAddress { get; set; }
        public int CreatedBy { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime? DateWedded { get; set; }
        [Display(Name = "Year Group")]
        public string YearGroupCode { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
