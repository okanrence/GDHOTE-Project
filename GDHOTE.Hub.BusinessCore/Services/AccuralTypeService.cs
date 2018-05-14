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
    public class AccuralTypeService : BaseService
    {
        public static string Save(AccuralType accuralType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(accuralType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert AccuralType";
            }
        }
        public static List<AccuralTypeViewModel> GetAllAccuralTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var countries = db.Fetch<AccuralTypeViewModel>()
                        .OrderBy(c => c.Name)
                        .ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccuralTypeViewModel>();
            }
        }
        public static List<AccuralTypeResponse> GetActiveAccuralTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accuralTypes = db.Fetch<AccuralType>()
                        .Where(c => c.StatusId == (int)CoreObject.Enumerables.Status.Active && c.DateDeleted == null)
                        .OrderBy(c => c.Name)
                        .ToList();
                    var item = JsonConvert.SerializeObject(accuralTypes);
                    var response = JsonConvert.DeserializeObject<List<AccuralTypeResponse>>(item);
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<AccuralTypeResponse>();
            }
        }
        public static AccuralType GetAccuralType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var accuralType = db.Fetch<AccuralType>()
                        .SingleOrDefault(b => b.Id == id);
                    return accuralType;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new AccuralType();
            }
        }
        public static string Update(AccuralType accuralType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(accuralType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update AccuralType";
            }
        }
        public static Response Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var accuralType = db.Fetch<AccuralType>().SingleOrDefault(c => c.Id == id);
                    if (accuralType == null)
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

                    //Delete AccuralType
                    accuralType.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    accuralType.DeletedById = user.Id;
                    accuralType.DateDeleted = DateTime.Now;
                    db.Update(accuralType);
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

        public static Response CreateAccuralType(CreateAccuralTypeRequest request, string currentUser)
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
                    var accuralTypeExist = db.Fetch<AccuralType>()
                        .SingleOrDefault(b => b.Name.ToLower().Equals(request.Name.ToLower()));
                    if (accuralTypeExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }

                    string accuralTypeName = StringCaseService.TitleCase(request.Name);

                    var accuralType = new AccuralType
                    {
                        Name = accuralTypeName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(accuralType);
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
