using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

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
                   var result= db.Insert(member);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to insert member");
            }
        }
        public static IEnumerable<Member> GetMembers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>();
                    return members;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to fetch member");
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
                throw new Exception("Error occured while trying to fetch member");
            }
        }
        public static int Update(Member member)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Update(member);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int Delete(int id)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var result = db.Delete<Member>(id);
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Log(EnumsService.LogType.Error, "", MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception("Error occured while trying to delete record");
            }
        }
    }
}
