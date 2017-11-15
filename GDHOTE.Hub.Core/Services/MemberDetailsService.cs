using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class MemberDetailsService : BaseService
    {
        public static string Save(MemberDetails memberDetails)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(memberDetails);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                if (ex.Message.Contains("The duplicate key")) return "Cannot Insert duplicate record";
                return "Error occured while trying to insert member";
            }
        }
        public static IEnumerable<MemberDetails> GetMembersDetails()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var membersdetails = db.Fetch<MemberDetails>().OrderBy(m => m.MemberKey);
                    return membersdetails;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<MemberDetails>();
            }
        }
        public static MemberDetails GetMemberDetails(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberDetails = db.Fetch<MemberDetails>().SingleOrDefault(m => m.MemberDetailsId == id);
                    return memberDetails;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new MemberDetails();
            }
        }
        public static MemberDetails GetMemberDetailsByMemberKey(int memberKey)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var memberDetails = db.Fetch<MemberDetails>().SingleOrDefault(m => m.MemberKey == memberKey);
                    return memberDetails;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new MemberDetails();
            }
        }
        public static string Update(MemberDetails memberDetails)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(memberDetails);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update member details";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<MemberDetails>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
    }
}
