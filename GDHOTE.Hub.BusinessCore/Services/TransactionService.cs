using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDHOTE.Hub.CoreObject.DataTransferObjects;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.ViewModels;

namespace GDHOTE.Hub.BusinessCore.Services
{
    public class TransactionService : BaseService
    {
        public static string Save(Transaction transaction)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(transaction);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert record";
            }
        }
        public static List<TransactionViewModel> GetTransactions(string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var transactions = db.Fetch<TransactionViewModel>()
                        .Where(p => p.DateCreated >= castStartDate && p.DateCreated < castEndDate.AddDays(1))
                        .ToList();
                    return transactions;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<TransactionViewModel>();
            }
        }
        public static List<TransactionViewModel> GetApprovedTransactions(string startdate, string enddate)
        {
            try
            {
                DateTime.TryParse(startdate, out var castStartDate);
                DateTime.TryParse(enddate, out var castEndDate);
                using (var db = GdhoteConnection())
                {
                    var transactions = db.Fetch<TransactionViewModel>()
                        .Where(p => p.DateCreated >= castStartDate && p.DateCreated < castEndDate.AddDays(1) && p.DateApproved != null)
                        .ToList();
                    return transactions;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new List<TransactionViewModel>();
            }
        }

        public static Transaction GetTransaction(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var transaction = db.Fetch<Transaction>().SingleOrDefault(p => p.Id == id);
                    return transaction;
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return new Transaction();
            }
        }

        public static string Update(Transaction transaction)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(transaction);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                return "Error occured while trying to update Transaction";
            }
        }
        public static Response Delete(ConfirmTransactionRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var response = new Response();

                    var transaction = db.Fetch<Transaction>()
                        .SingleOrDefault(t => t.TransactionReference == request.TransactionReference);
                    if (transaction == null)
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


                    //Delete Transaction
                    transaction.Remarks = request.Comment;
                    transaction.TransactionStatusId = (int)CoreObject.Enumerables.TransactionStatus.Cancelled;
                    transaction.DeletedById = user.Id;
                    transaction.DateDeleted = DateTime.Now;
                    db.Update(transaction);
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

        public static Response ConfirmTransaction(ConfirmTransactionRequest request, string currentUser)
        {
            try
            {
                using (var db = GdhoteConnection())
                {

                    var response = new Response();

                    var transaction = db.Fetch<Transaction>()
                        .SingleOrDefault(p => p.TransactionReference == request.TransactionReference);
                    if (transaction == null)
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
                    var action = request.Action;

                    transaction.Remarks = request.Comment;
                    transaction.TransactionStatusId = action == "S"
                        ? (int)CoreObject.Enumerables.PaymentStatus.Approved
                        : (int)CoreObject.Enumerables.PaymentStatus.Deleted;
                    transaction.DeletedById = user.Id;
                    transaction.DateDeleted = DateTime.Now;
                    db.Update(transaction);

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
                LogService.LogError(ex.Message);
                return new Response
                {
                    ErrorCode = "01",
                    ErrorMessage = "Error occured while trying to Confirm Payment"
                };
            }
        }
    }
}
