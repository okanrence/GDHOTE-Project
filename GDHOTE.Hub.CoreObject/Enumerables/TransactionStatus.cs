using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.CoreObject.Enumerables
{
    public enum TransactionStatus
    {
        Successful = 1,
        Pending = 2,
        Cancelled = 3,
        Declined = 4,
        Deferred = 5,
        Refunded = 6
    }
}
