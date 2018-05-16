using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;
using Newtonsoft.Json;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class AccrualTypeService : BaseService
    {
        public static string Save(AccrualType accrualType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(accrualType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert AccrualType";
            }
        }
        public static List<AccrualTypeViewModel> GetAllAccrualTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accrualTypes = db.Fetch<AccrualTypeViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return accrualTypes;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccrualTypeViewModel>();
            }
        }
        public static List<AccrualTypeResponse> GetActiveAccrualTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accrualTypes = db.Fetch<AccrualType>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    var item = JsonConvert.SerializeObject(accrualTypes);
                    var response = JsonConvert.DeserializeObject<List<AccrualTypeResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccrualTypeResponse>();
            }
        }
        public static AccrualType GetAccrualType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accrualType = db.Fetch<AccrualType>()
                        .SingleOrDefault(b => b.Id == id);
                    return accrualType;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new AccrualType();
            }
        }
        public static string Update(AccrualType accrualType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(accrualType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update AccrualType";
            }
        }
        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var accrualType = db.Fetch<AccrualType>().SingleOrDefault(c => c.Id == id);
                    if (accrualType == null)
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

                    //Delete AccrualType
                    accrualType.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    accrualType.DeletedById = user.Id;
                    accrualType.DateDeleted = DateTime.Now;
                    db.Update(accrualType);
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

        public static Response CreateAccrualType(CreateAccrualTypeRequest request, string currentUser)
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
                    var accrualTypeExist = db.Fetch<AccrualType>()
                        .SingleOrDefault(b => b.Name.ToLower().Equals(request.Name.ToLower()));
                    if (accrualTypeExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string accrualTypeName = StringCaseService.TitleCase(request.Name);

                    var accrualType = new AccrualType
                    {
                        Name = accrualTypeName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(accrualType);
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
