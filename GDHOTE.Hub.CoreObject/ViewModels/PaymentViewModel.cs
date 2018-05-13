using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_Payments")]
    public class PaymentViewModel : Payment
    {
        public string MemberCode { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string PaymentType { get; set; }
        public string PaymentMode { get; set; }
        public string Currency { get; set; }
    }
}
