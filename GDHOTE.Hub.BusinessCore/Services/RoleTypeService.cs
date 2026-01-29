using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using Newtonsoft.Json;

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

        public static List<RoleTypeResponse> GetActiveRoleTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleTypes = db.Fetch<RoleType>()
                        .ToList();
                    var item = JsonConvert.SerializeObject(roleTypes);
                    var response = JsonConvert.DeserializeObject<List<RoleTypeResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleTypeResponse>();
            }
        }
    }
}
