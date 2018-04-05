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
    public class PaymentModeService : BaseService
    {
        public static string Save(PaymentMode paymentMode)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(paymentMode);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert payment mode";
            }
        }
        public static List<PaymentModeViewModel> GetAllPaymentModes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentModes = db.Fetch<PaymentModeViewModel>()
                        .OrderBy(pm => pm.Name).ToList();
                    return paymentModes;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PaymentModeViewModel>();
            }
        }
        public static List<PaymentMode> GetActivePaymentModes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentModes = db.Fetch<PaymentMode>()
                        .Where(pm => pm.StatusId == (int)CoreObject.Enumerables.Status.Active && pm.DateDeleted == null)
                        .OrderBy(pm => pm.Name)
                        .ToList();
                    return paymentModes;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<PaymentMode>();
            }
        }
        public static PaymentMode GetPaymentMode(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentMode = db.Fetch<PaymentMode>()
                        .SingleOrDefault(pm => pm.Id == id);
                    return paymentMode;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new PaymentMode();
            }
        }
        public static string Update(PaymentMode paymentMode)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(paymentMode);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update mode of payment";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentMode = db.Fetch<PaymentMode>().SingleOrDefault(c => c.Id == id);
                    if (paymentMode == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);

                    //Delete Payment Mode
                    paymentMode.StatusId = (int)CoreObject.Enumerables.Status.Deleted;
                    paymentMode.DeletedById = user.UserId;
                    paymentMode.DateDeleted = DateTime.Now;
                    db.Update(paymentMode);
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

        public static Response CreatePaymentMode(CreatePaymentModeRequest request, string currentUser)
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
                    var paymentModeExist = db.Fetch<PaymentMode>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (paymentModeExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string modeName = StringCaseManager.TitleCase(request.Name);

                    var paymentMode = new PaymentMode
                    {
                        Name = modeName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(paymentMode);
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
