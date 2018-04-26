using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class MemberDetailsResponse
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
        public string HighestDegreeObtained { get; set; }
        public string CurrentWorkPlace { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
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
