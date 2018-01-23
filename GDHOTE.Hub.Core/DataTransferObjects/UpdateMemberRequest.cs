using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.Core.DataTransferObjects
{
    public class UpdateMemberRequest
    {
        public int MemberKey { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public bool InitiationFlag { get; set; }
        public bool MagusFlag { get; set; }
        public string MartialStatus { get; set; }
        public string StatusCode { get; set; }
        public string DeleteFlag { get; set; }
        public string ApprovedFlag { get; set; }
        //public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? MagusDate { get; set; }
    }
}
