using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class MemberResponse
    {
        public long Id { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MemberStatusId { get; set; }
        public bool InitiationStatus { get; set; }
        public bool MagusStatus { get; set; }
        public DateTime? InitiationDate { get; set; }
        public DateTime? MagusDate { get; set; }
        public int ChannelId { get; set; }
        public string ApprovedFlag { get; set; }
        public string ApprovedById { get; set; }
        public int OfficerId { get; set; }
        public DateTime? OfficerDate { get; set; }
    }
}
