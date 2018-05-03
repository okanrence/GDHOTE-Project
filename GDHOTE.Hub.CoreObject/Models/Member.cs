using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace GDHOTE.Hub.CoreObject.Models
{
    [TableName("HUB_Members")]
    public class Member : BaseModel
    {
        public long Id { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int MemberStatusId { get; set; }
        public bool InitiationStatus { get; set; }
        public bool MagusStatus { get; set; }
        public DateTime? InitiationDate { get; set; }
        public DateTime? MagusDate { get; set; }
        public int ChannelId { get; set; }
        public string ApprovedFlag { get; set; }
        public long ApprovedById { get; set; }
        public int OfficerId { get; set; }
        public DateTime? OfficerDate { get; set; }
        public DateTime? DateApproved { get; set; }
        public string MemberKey { get; set; }
    }
}
