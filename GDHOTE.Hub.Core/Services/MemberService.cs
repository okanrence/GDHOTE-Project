using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;
using GDHOTE.Hub.Core.Enumerables;
using GDHOTE.Hub.Core.Exceptions;

namespace GDHOTE.Hub.Core.Services
{
    public class MemberService : BaseService
    {
        public static string Save(Member member)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Insert(member);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new UnableToCompleteException(ex.Message, MethodBase.GetCurrentMethod().Name);
            }
        }
        public static IEnumerable<Member> GetMembers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>().OrderBy(m => m.MemberKey);
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Member>();
            }
        }
        public static Member CheckIfMemberExist(Member memberRequest)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<Member>().
                        SingleOrDefault(m => m.Surname == memberRequest.Surname
                        && m.FirstName == memberRequest.FirstName);
                    return member;
                }
            }
            catch (Exception ex)
            {
                //LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                throw new UnableToCompleteException(ex.Message, MethodBase.GetCurrentMethod().Name);
            }
        }
        public static Member GetMember(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<Member>().SingleOrDefault(m => m.MemberKey == id);
                    return member;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new Member();
            }
        }
        public static string Update(Member member)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(member);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to update member";
            }
        }
        public static string Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Member>(id);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return "Error occured while trying to delete record";
            }
        }
        public static IEnumerable<Member> GetMembersPendingApproval()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>().Where(m => m.ApprovedFlag == "N").OrderBy(m => m.MemberKey);
                    return members;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                return new List<Member>();
            }
        }
    }
}
