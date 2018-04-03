using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDHOTE.Hub.CoreObject.Enumerables
{
    public enum EmailType
    {
        RegistrationConfirmation = 1,
        PaymentConfirmation = 2,
        PasswordReset = 3,
        BirthdayNotification = 4,
        WeddingAnniversaryNotification = 5
    }
}
