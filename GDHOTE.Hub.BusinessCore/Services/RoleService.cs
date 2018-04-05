using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using NPoco;
using NPoco.Expressions;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class RoleService : BaseService
    {

        public static string Save(Role role)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(role);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert record";
            }
        }
        public static List<RoleViewModel> GetAllRoles()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<RoleViewModel>()
                        .OrderBy(r => r.RoleName)
                        .ToList();
                    return roles;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleViewModel>();
            }
        }
        public static List<Role> GetActiveRoles()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<Role>()
                        .Where(r => r.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .ToList();
                    return roles;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Role>();
            }
        }

        public static List<Role> GetRolesByRoleType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<Role>()
                        .Where(r => r.RoleTypeId == id && r.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .ToList();
                    return roles;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<Role>();
            }
        }
        public static Role GetRole(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var role = db.Fetch<Role>().SingleOrDefault(s => s.RoleId == id);
                    return role;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Role();
            }
        }

        public static string Update(Role role)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(role);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Role";
            }
        }
        public static string Delete(string roleId, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var role = db.Fetch<Role>().SingleOrDefault(c => c.RoleId == roleId);
                    if (role == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Payment Mode
                    role.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    role.DeletedById = user.UserId;
                    role.DateDeleted = DateTime.Now;
                    db.Update(role);
                    var result = "Operation Successful";
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to delete record";
            }
        }
        public static Response CreateRole(CreateRoleRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Unable to validate User"
                        };
                    }

                    //Check if name already exist
                    var roleExist = db.Fetch<Role>()
                        .SingleOrDefault(c => c.RoleName.ToLower().Equals(request.RoleName.ToLower()));
                    if (roleExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string roleName = StringCaseManager.TitleCase(request.RoleName);

                    var role = new Role
                    {
                        RoleId = Guid.NewGuid().ToString(),
                        RoleName = roleName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(role);
                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Successful"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }

        }
    }
}
