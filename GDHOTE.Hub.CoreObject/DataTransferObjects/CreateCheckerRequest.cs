using System;
namespace GDHOTE.Hub.CoreObject.DataTransferObjects
{
    public class CreateCheckerRequest
    {
        public string ApplicationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
