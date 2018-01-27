using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Enumerables;

namespace GDHOTE.Hub.Core.Services
{
    public class SubMenuViewService : BaseService
    {
        public static IEnumerable<SubMenuView> GetSubMenusView()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var subMenus = db.Fetch<SubMenuView>();
                    return subMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<SubMenuView>();
            }
        }
    }
}
