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

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class PaymentTypeService : BaseService
    {
        public static string Save(PaymentType paymentType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(paymentType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert payment types";
            }
        }
        public static List<PaymentType> GetAllPaymentTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentTypes = db.Fetch<PaymentType>()
                        .OrderBy(p => p.Name).ToList();
                    return paymentTypes;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<PaymentType>();
            }
        }
        public static List<PaymentType> GetActivePaymentTypes()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentTypes = db.Fetch<PaymentType>()
                        .Where(p => p.StatusId == (int)CoreObject.Enumerables.Status.Active && p.DateDeleted == null)
                        .OrderBy(p => p.Name).ToList();
                    return paymentTypes;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new List<PaymentType>();
            }
        }

        public static PaymentType GetPaymentType(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var paymentType = db.Fetch<PaymentType>().SingleOrDefault(p => p.Id == id);
                    return paymentType;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return new PaymentType();
            }
        }
        public static string Update(PaymentType paymentType)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(paymentType);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to update payment";
            }
        }
        public static string Delete(int id, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var paymentType = db.Fetch<PaymentType>().SingleOrDefault(c => c.Id == id);
                    if (paymentType == null)
                    {
                        return "Record does not exist";
                    }

                    //Get User Initiating Creation Request
                    var user = UserService.GetUserByUserName(currentUser);


                    //Delete Payment Type
                    paymentType.StatusId = (int)CoreObject.Enumerables.Status.DeActivated;
                    paymentType.DeletedById = user.UserId;
                    paymentType.DateDeleted = DateTime.Now;
                    db.Update(paymentType);
                    var result = "Operation Successful";
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(ex.Message);
                return "Error occured while trying to delete record";
            }
        }


        public static Response CreatePaymentType(CreatePaymentTypeRequest request, string currentUser)
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
                    var paymentTypeExist = db.Fetch<PaymentType>()
                        .SingleOrDefault(c => c.Name.ToLower().Equals(request.Name.ToLower()));
                    if (paymentTypeExist != null)
                    {
                        return new Response
                        {
                            ErrorCode = "01",
                            ErrorMessage = "Record already exist"
                        };
                    }


                    string typeName = StringCaseManager.TitleCase(request.Name);


                    var paymentType = new PaymentType
                    {
                        Name = typeName,
                        StatusId = (int)CoreObject.Enumerables.Status.Active,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(paymentType);
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
                LogService.Log(ex.Message);
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
