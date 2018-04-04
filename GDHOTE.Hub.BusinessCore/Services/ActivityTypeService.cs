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
    public class ActivityTypeService : BaseService
    {
        public static string Save(ActivityType activityType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(activityType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert ActivityType";
            }
        }
        public static List<ActivityTypeViewModel> GetAllActivityTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<ActivityTypeViewModel>()
                        .OrderBy(c => c.Name).ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<ActivityTypeViewModel>();
            }
        }
        public static List<ActivityType> GetActivityTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<ActivityType>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name).ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<ActivityType>();
            }
        }
        public static ActivityType GetActivityType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activityType = db.Fetch<ActivityType>().SingleOrDefault(c => c.Id == id);
                    return activityType;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new ActivityType();
            }
        }
        public static string Update(ActivityType activityType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(activityType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return "Error occured while trying to update ActivityType";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var activityType = db.Fetch<ActivityType>().SingleOrDefault(c => c.Id == id);
                    if (activityType == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Bank
                    activityType.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    activityType.DeletedById = user.UserId;
                    activityType.DateDeleted = DateTime.Now;
                    db.Update(activityType);
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

        public static Response CreateActivityType(CreateActivityTypeRequest request, string currentUser)
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
                    var activityTypeExist = db.Fetch<ActivityType>().SingleOrDefault(a => a.Name.ToLower().Equals(request.Name.ToLower()));
                    if (activityTypeExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string name = StringCaseManager.TitleCase(request.Name);

                    var activityType = new ActivityType
                    {
                        Name = name,
                        DependencyTypeId = request.DependencyTypeId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(activityType);
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
