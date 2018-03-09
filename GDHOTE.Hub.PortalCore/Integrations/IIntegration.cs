using System;
using System.Collections.Generic;
using System.Text;

namespace GDHOTE.Hub.PortalCore.Integrations
{
    public interface IIntegration<T>
    {
        T Invoke();
    }
}
