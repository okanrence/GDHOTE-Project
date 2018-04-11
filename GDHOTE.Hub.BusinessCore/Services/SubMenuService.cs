using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class SubMenuService : BaseService
    {
        public static string Save(SubMenu subMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(subMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert SubMenu";
            }
        }
        public static List<SubMenuViewModel> GetAllSubMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var subMenus = db.Fetch<SubMenuViewModel>()
                        .OrderBy(c => c.DisplaySequence)
                        .ToList();
                    return subMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<SubMenuViewModel>();
            }
        }
        public static List<SubMenu> GetActiveSubMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var subMenus = db.Fetch<SubMenu>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .OrderBy(c => c.DisplaySequence)
                        .ToList();
                    return subMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<SubMenu>();
            }
        }

        public static List<SubMenu> GetSubMenusByMainMenu(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var subMenus = db.Fetch<SubMenu>()
                        .Where(c => c.MenuId == id && c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .OrderBy(c => c.DisplaySequence)
                        .ToList();
                    return subMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<SubMenu>();
            }
        }
        public static SubMenu GetSubMenu(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var subMenu = db.Fetch<SubMenu>().SingleOrDefault(c => c.Id == id);
                    return subMenu;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new SubMenu();
            }
        }
        public static string Update(SubMenu subMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(subMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update SubMenu";
            }
        }
        public static Response Delete(string id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var subMenu = db.Fetch<SubMenu>().SingleOrDefault(c => c.Id == id);
                    if (subMenu == null)
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
                    subMenu.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    subMenu.DeletedById = user.UserId;
                    subMenu.DateDeleted = DateTime.Now;
                    db.Update(subMenu);
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


        public static Response CreateSubMenu(CreateSubMenuRequest request, string currentUser)
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



                    string name = StringCaseManager.TitleCase(request.Name);

                    var subMenu = new SubMenu
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = name,
                        DisplaySequence = request.DisplaySequence,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        MenuId = request.MenuId,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(subMenu);
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
    }
}
