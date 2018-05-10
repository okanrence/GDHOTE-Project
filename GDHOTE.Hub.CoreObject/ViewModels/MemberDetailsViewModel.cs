using System;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_MemberDetails")]
    public class MemberDetailsViewModel : MemberDetails
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string MemberCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime? MagusDate { get; set; }
        public string MaritalStatus { get; set; }
        public string MagusStatus { get; set; }
        public string InitiationStatus { get; set; }
    }
}
