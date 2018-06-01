﻿using System;
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
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
                return new List<ActivityType>();
            }
        }
        public static ActivityType GetActivityType(string activityTypeKey)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var activityType = db.Fetch<ActivityType>()
                        .SingleOrDefault(a => a.ActivityTypeKey == activityTypeKey);
                    return activityType;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
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
                LogService.LogError(ex.Message);
                return "Error occured while trying to update ActivityType";
            }
        }
        public static Response Delete(string activityTypeKey, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var activityType = db.Fetch<ActivityType>()
                        .SingleOrDefault(c => c.ActivityTypeKey == activityTypeKey);
                    if (activityType == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record does not exist"
                        };
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Bank
                    activityType.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    activityType.DeletedById = user.Id;
                    activityType.DateDeleted = DateTime.Now;
                    db.Update(activityType);
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

                    string name = StringCaseService.TitleCase(request.Name);

                    var activityType = new ActivityType
                    {
                        Name = name,
                        DependencyTypeId = request.DependencyTypeId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        ActivityTypeKey = Guid.NewGuid().ToString(),
                        CreatedById = user.Id,
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
                LogService.LogError(ex.Message);
                var response = new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
                return response;
            }

        }


        public static Response UpdateActivityType(UpdateActivityTypeRequest updateRequest, string currentUser)
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

                    //Check member exist
                    var activityType = db.Fetch<ActivityType>()
                        .SingleOrDefault(a => a.ActivityTypeKey == updateRequest.ActivityTypeKey);

                    if (activityType == null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record not found"
                        };
                    }

                    string name = StringCaseService.TitleCase(updateRequest.Name);
                    activityType.Name = name;
                    activityType.DependencyTypeId = updateRequest.DependencyTypeId;
                    activityType.StatusId = updateRequest.StatusId;
                    activityType.UpdatedById = user.Id;
                    activityType.DateUpdated = DateTime.Now;
                    db.Update(activityType);
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
