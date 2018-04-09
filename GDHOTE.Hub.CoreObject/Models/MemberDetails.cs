using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_MemberDetails")]
    public class MemberDetails : BaseModel
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string EmailAddress { get; set; }
        public int StateOfOriginId { get; set; }
        public int CountryOfOriginId { get; set; }
        public int ResidenceStateId { get; set; }
        public int ResidenceCountryId { get; set; }
        public int MemberStatusId { get; set; }
        public string ResidenceAddress { get; set; }
        public DateTime? DateWedded { get; set; }
        public int YearGroupId { get; set; }
        public string GuardianAngel { get; set; }

    }
}
