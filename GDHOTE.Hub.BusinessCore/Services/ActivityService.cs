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
    public class ActivityService : BaseService
    {
        public static string Save(Activity activity)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(activity);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert Activity";
            }
        }
        public static List<ActivityViewModel> GetAllActivities(string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var activities = db.Fetch<ActivityViewModel>()
                        .Where(a => a.DateCreated >= castStartDate && a.DateCreated < castEndDate.AddDays(1))
                        .OrderBy(a => a.FirstName).ToList();
                    return activities;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<ActivityViewModel>();
            }
        }
        public static List<ActivityViewModel> GetMemberActivities(int memberId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activities = db.Fetch<ActivityViewModel>()
                        .Where(a => a.MemberId == memberId)
                        .OrderBy(a => a.StartDate)
                        .ToList();
                    return activities;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<ActivityViewModel>();
            }
        }
        public static Activity GetActivity(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activity = db.Fetch<Activity>()
                        .SingleOrDefault(c => c.Id == id);
                    return activity;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Activity();
            }
        }
        public static string Update(Activity activity)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(activity);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Activity";
            }
        }


        public static Response CreateActivity(CreateActivityRequest request, string currentUser)
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


                    var activity = new Activity
                    {
                        ActivityTypeId = request.ActivityTypeId,
                        MemberId = request.MemberId,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(activity);
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

        public static Response Delete(long id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var activity = db.Fetch<Activity>().SingleOrDefault(c => c.Id == id);
                    if (activity == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "User does not exist"
                        };
                    }


                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);
                    if (user == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "User does not exist"
                        };
                    }


                    //Delete Bank
                    activity.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    activity.DeletedById = user.Id;
                    activity.DateDeleted = DateTime.Now;
                    db.Update(activity);
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

        public static List<ActivityViewModel> GetActivitiesByCriteria(int criteria, string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var activities = new List<ActivityViewModel>();
                    switch (criteria)
                    {
                        case 1:
                            activities = db.Fetch<ActivityViewModel>()
                              .Where(m => m.StartDate >= castStartDate && m.EndDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                              .ToList();
                            break;
                        case 2:
                            activities = db.Fetch<ActivityViewModel>()
                                .Where(m => m.StartDate >= castStartDate && m.EndDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                                .ToList();
                            break;
                        case 3:
                            activities = db.Fetch<ActivityViewModel>()
                                .Where(m => m.StartDate >= castStartDate && m.EndDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                                .ToList();
                            break;
                        case 4:
                            activities = db.Fetch<ActivityViewModel>()
                                .Where(m => m.StartDate >= castStartDate && m.EndDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                                .ToList();
                            break;
                        default:
                            activities = db.Fetch<ActivityViewModel>()
                                .Where(m => m.RecordDate >= castStartDate && m.RecordDate < castEndDate.AddDays(1))
                                .OrderBy(m => m.FirstName).ThenBy(m => m.Surname)
                                .ToList();
                            break;

                    }
                    return activities;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<ActivityViewModel>();
            }
        }
    }
}
