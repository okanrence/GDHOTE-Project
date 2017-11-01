using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.Core.Models
{
    [TableName("HUB_MemberDetails")]
    [PrimaryKey("MemberKey")]
    public class MemberDetails
    {
        public int MemberDetailsId { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string StateOfOrigin { get; set; }
        public string CountryOfOrigin { get; set; }
        public string ResidenceAddress { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
