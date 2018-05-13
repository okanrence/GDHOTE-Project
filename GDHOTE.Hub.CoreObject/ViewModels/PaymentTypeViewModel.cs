using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_PaymentTypes")]
    public class PaymentTypeViewModel : PaymentType
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Status   { get; set; }
        public string CreatedBy { get; set; }
    }
}
