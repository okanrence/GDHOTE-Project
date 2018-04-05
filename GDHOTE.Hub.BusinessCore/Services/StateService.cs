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
    public class StateService : BaseService
    {
        public static string Save(State state)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(state);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert state";
            }
        }

        public static List<StateViewModel> GetAllStates()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var states = db.Fetch<StateViewModel>()
                        .OrderBy(s => s.Name)
                        .ToList();
                    return states;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<StateViewModel>();
            }
        }
        public static List<State> GetActiveStates()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var states = db.Fetch<State>()
                        .Where(s => s.StatusId == (int)CoreObject.Enumerables.Status.Active && s.DateDeleted == null)
                        .OrderBy(s => s.Name)
                        .ToList();
                    return states;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<State>();
            }
        }
        public static List<State> GetStatesByCountry(int countryId)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var states = db.Fetch<State>()
                        .Where(s => s.CountryId == countryId)
                        .OrderBy(s => s.Name)
                        .ToList();
                    return states;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<State>();
            }
        }
        public static State GetState(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var state = db.Fetch<State>().SingleOrDefault(s => s.Id == id);
                    return state;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new State();
            }
        }
        public static string Update(State state)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(state);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update state";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var state = db.Fetch<State>().SingleOrDefault(c => c.Id == id);
                    if (state == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Country
                    state.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    state.DeletedById = user.UserId;
                    state.DateDeleted = DateTime.Now;
                    db.Update(state);
                    var result = "Operation Successful";
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to delete record";
            }
        }

        public static Response CreateState(CreateStateRequest request, string currentUser)
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
                    var stateExist = db.Fetch<State>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (stateExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string stateName = StringCaseManager.TitleCase(request.Name);

                    var state = new State
                    {
                        Name = stateName,
                        CountryId= request.CountryId,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(state);
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
