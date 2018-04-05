using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.Models;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class RoleTypeService : BaseService
    {
        public static List<RoleType> GetRoleTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleTypes = db.Fetch<RoleType>()
                        .ToList();
                    return roleTypes;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleType>();
            }
        }
    }
}
