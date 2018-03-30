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
    [PrimaryKey("MemberKey")]
    public class Member : BaseModel
    {
        public int MemberKey { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public bool InitiationFlag { get; set; }
        public bool MagusFlag { get; set; }
        public string MaritalStatus { get; set; }
        public int MemberStatusId { get; set; }
        public int ChannelId { get; set; }
        public string ApprovedFlag { get; set; }
        public string ApprovedById { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? MagusDate { get; set; }
        public DateTime? DateApproved { get; set; }
        public int OfficerId { get; set; }
        public DateTime OfficerDate { get; set; }
    }
}
