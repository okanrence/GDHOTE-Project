using System;
using System.Collections.Generic;
using System.Linq;
using GDHOTE.Hub.BusinessCore.BusinessLogic;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class MainMenuService : BaseService
    {
        public static string Save(MainMenu mainMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(mainMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert MainMenu";
            }
        }
        public static List<MainMenuViewModel> GetAllMainMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var mainMenus = db.Fetch<MainMenuViewModel>()
                        .OrderBy(c => c.DisplaySequence)
                        .ToList();
                    return mainMenus;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MainMenuViewModel>();
            }
        }
        public static List<MainMenuResponse> GetActiveMainMenus()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var mainMenus = db.Fetch<MainMenu>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active)
                        .OrderBy(c => c.DisplaySequence)
                        .ToList();
                    var item = JsonConvert.SerializeObject(mainMenus);
                    var response = JsonConvert.DeserializeObject<List<MainMenuResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<MainMenuResponse>();
            }
        }
        public static MainMenu GetMainMenu(string id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var mainMenu = db.Fetch<MainMenu>().SingleOrDefault(c => c.Id == id);
                    return mainMenu;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new MainMenu();
            }
        }
        public static string Update(MainMenu mainMenu)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(mainMenu);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update MainMenu";
            }
        }

        public static Response CreateMainMenu(CreateMainMenuRequest request, string currentUser)
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



                    string name = StringCaseService.TitleCase(request.Name);

                    var mainMenu = new MainMenu
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = name,
                        DisplaySequence = request.DisplaySequence,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(mainMenu);
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

                    var mainMenu = db.Fetch<MainMenu>().SingleOrDefault(c => c.Id == id);
                    if (mainMenu == null)
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



                    //Delete MainMenu
                    mainMenu.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    mainMenu.DeletedById = user.Id;
                    mainMenu.DateDeleted = DateTime.Now;
                    db.Update(mainMenu);
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
