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
using Newtonsoft.Json;

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
                        .OrderBy(r => r.Name)
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
        public static List<RoleResponse> GetActiveRoles()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<Role>()
                        .Where(r => r.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .ToList();
                    var item = JsonConvert.SerializeObject(roles);
                    var response = JsonConvert.DeserializeObject<List<RoleResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleResponse>();
            }
        }

        public static List<RoleResponse> GetRolesByRoleType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roles = db.Fetch<Role>()
                        .Where(r => r.RoleTypeId == id && r.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .ToList();
                    var item = JsonConvert.SerializeObject(roles);
                    var response = JsonConvert.DeserializeObject<List<RoleResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleResponse>();
            }
        }
        public static Role GetRole(string roleKey)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var role = db.Fetch<Role>().SingleOrDefault(s => s.RoleKey == roleKey);
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
        public static Response Delete(string roleKey, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var role = db.Fetch<Role>().SingleOrDefault(c => c.RoleKey == roleKey);
                    if (role == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }

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

                    //Delete Payment Mode
                    role.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    role.DeletedById = user.Id;
                    role.DateDeleted = DateTime.Now;
                    db.Update(role);
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
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to delete record"
                };
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
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (roleExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string roleName = StringCaseService.TitleCase(request.Name);
                    var role = new Role
                    {
                        RoleKey = Guid.NewGuid().ToString(),
                        Name = roleName,
                        RoleTypeId = request.RoleTypeId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
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
