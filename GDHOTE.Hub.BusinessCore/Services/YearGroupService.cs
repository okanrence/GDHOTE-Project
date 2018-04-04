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
    public class YearGroupService : BaseService
    {
        public static string Save(YearGroup yearGroup)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(yearGroup);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert YearGroup";
            }
        }
        public static List<YearGroupViewModel> GetAllYearGroups()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var yearGroups = db.Fetch<YearGroupViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return yearGroups;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<YearGroupViewModel>();
            }
        }
        public static List<YearGroup> GetActiveYearGroups()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var yearGroups = db.Fetch<YearGroup>()
                        .Where(y => y.StatusId == (int)CoreObject.Enumerables.Status.Active && y.DateDeleted == null)
                        .OrderBy(y => y.Name)
                        .ToList();
                    return yearGroups;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<YearGroup>();
            }
        }
       
        public static YearGroup GetYearGroup(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var yearGroup = db.Fetch<YearGroup>().SingleOrDefault(c => c.Id == id);
                    return yearGroup;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new YearGroup();
            }
        }
        public static string Update(YearGroup yearGroup)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(yearGroup);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return "Error occured while trying to update YearGroup";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var yearGroup = db.Fetch<YearGroup>().SingleOrDefault(c => c.Id == id);
                    if (yearGroup == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);



                    //Delete Year Group
                    yearGroup.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    yearGroup.DeletedById = user.UserId;
                    yearGroup.DateDeleted = DateTime.Now;
                    db.Update(yearGroup);
                    var result = "Operation Successful";
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return "Error occured while trying to delete record";
            }
        }


        public static Response CreateYearGroup(CreateYearGroupRequest request, string currentUser)
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
                    var yearGroupExist = db.Fetch<YearGroup>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (yearGroupExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string yearName = StringCaseManager.TitleCase(request.Name);


                    var yearGroup = new YearGroup
                    {
                        Name = yearName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(yearGroup);
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
                LogService.myLog(ex.Message);
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
