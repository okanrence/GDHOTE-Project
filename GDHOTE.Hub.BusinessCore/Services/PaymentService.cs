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
    public class PaymentService : BaseService
    {
        public static string Save(Payment payment)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(payment);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert record";
            }
        }
        public static List<PaymentViewModel> GetPayments(string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var payments = db.Fetch<PaymentViewModel>()
                        .Where(p => p.DateCreated >= castStartDate && p.DateCreated < castEndDate.AddDays(1))
                        .ToList();
                    return payments;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<PaymentViewModel>();
            }
        }
        public static List<PaymentViewModel> GetApprovedPayments(string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var payments = db.Fetch<PaymentViewModel>()
                        .Where(p => p.DateCreated >= castStartDate && p.DateCreated < castEndDate.AddDays(1) && p.DateApproved != null)
                        .ToList();
                    return payments;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<PaymentViewModel>();
            }
        }


        public static List<PaymentViewModel> GetPaymentsPendingApproval()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var payments = db.Fetch<PaymentViewModel>()
                        .Where(p => p.DateApproved == null)
                        .ToList();
                    return payments;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new List<PaymentViewModel>();
            }
        }
        public static Payment GetPayment(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var payment = db.Fetch<Payment>().SingleOrDefault(p => p.Id == id);
                    return payment;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new Payment();
            }
        }

        public static string Update(Payment payment)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(payment);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return "Error occured while trying to update payment";
            }
        }
        public static Response Delete(ConfirmPaymentRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response= new Response();

                    var payment = db.Fetch<Payment>().SingleOrDefault(c => c.Id == request.PaymentId);
                    if (payment == null)
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


                    //Delete Payment
                    payment.Remarks = request.Comment;
                    payment.PaymentStatusId = (int)CoreObject.Enumerables.PaymentStatus.Deleted;
                    payment.DeletedById = user.UserId;
                    payment.DateDeleted = DateTime.Now;
                    db.Update(payment);
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
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to delete record"
                };
            }
        }


        public static Response CreatePayment(CreatePaymentRequest request, string currentUser)
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



                    string narration = StringCaseManager.TitleCase(request.Narration);

                    var payment = new Payment
                    {
                        MemberKey = request.MemberKey,
                        Amount = request.Amount,
                        PaymentTypeId = request.PaymentTypeId,
                        PaymentModeId = request.PaymentModeId,
                        CurrencyId = request.CurrencyId,
                        Narration = narration,
                        PaymentStatusId = (int)CoreObject.Enumerables.PaymentStatus.New,
                        CreatedById = user.UserId,
                        DateCreated = DateTime.Now,
                        RecordDate = DateTime.Now
                    };

                    db.Insert(payment);
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
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to insert record"
                };
            }

        }


        public static Response ConfirmPayment(ConfirmPaymentRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var payment = db.Fetch<Payment>().SingleOrDefault(c => c.Id == request.PaymentId);
                    if (payment == null)
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
                            ErrorMessage = "User does not exist"
                        };
                    }


                    //Update Payment
                    //var action = request.Action;

                    payment.Remarks = request.Comment;
                    payment.PaymentStatusId = (int)CoreObject.Enumerables.PaymentStatus.Deleted;
                    payment.DeletedById = user.UserId;
                    payment.DateDeleted = DateTime.Now;
                    db.Update(payment);

                    response = new Response
                    {
                        ErrorCode = "00",
                        ErrorMessage = "Operation Successful"
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogService.myLog(ex.Message);
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to Confirm Payment"
                };
            }
        }
    }
}
