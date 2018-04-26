using GDHOTE.Hub.CoreObject.Models;
using NPoco;

namespace GDHOTE.Hub.CoreObject.ViewModels
{
    [TableName("vx_HUB_Currencies")]
    public class CurrencyViewModel : Currency
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }
}
