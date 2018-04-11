using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class RoleMenuService : BaseService
    {
        public static string Save(RoleMenu roleMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(roleMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert RoleMenu";
            }
        }
        public static List<RoleMenuViewModel> GetAllRoleMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleMenus = db.Fetch<RoleMenuViewModel>()
                        .ToList();
                    return roleMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleMenuViewModel>();
            }
        }
        public static List<RoleMenu> GetActiveRoleMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleMenus = db.Fetch<RoleMenu>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .ToList();
                    return roleMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<RoleMenu>();
            }
        }

        public static RoleMenu GetRoleMenu(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var roleMenu = db.Fetch<RoleMenu>().SingleOrDefault(c => c.Id == id);
                    return roleMenu;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new RoleMenu();
            }
        }
        public static string Update(RoleMenu roleMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(roleMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update RoleMenu";
            }
        }


        public static Response CreateRoleMenu(CreateRoleMenuRequest request, string currentUser)
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

                    //Check to menu doesnt exist for Role
                    var roleExist = db.Fetch<RoleMenu>().SingleOrDefault(r => r.SubMenuId == request.SubMenuId && r.RoleId == request.RoleId);
                    if (roleExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    var roleMenu = new RoleMenu
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = request.RoleId,
                        SubMenuId = request.SubMenuId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(roleMenu);
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
                    ErrorMessage = "Error occured while trying to insert record"
                };
            }

        }


        public static Response Delete(string id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var roleMenu = db.Fetch<RoleMenu>().SingleOrDefault(c => c.Id == id);
                    if (roleMenu == null)
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
                            ErrorMessage = "Record does not exist"
                        };
                    }


                    //Delete Sub Menu
                    roleMenu.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    roleMenu.DeletedById = user.UserId;
                    roleMenu.DateDeleted = DateTime.Now;
                    db.Update(roleMenu);
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
    }
}
