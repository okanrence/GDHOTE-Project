using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.CoreObject.Models;
using GDHOTE.Hub.CoreObject.Enumerables;
using GDHOTE.Hub.BusinessCore.Exceptions;

namespace GDHOTE.Hub.BusinessCore.Services
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
        public static List<Member> GetMembers()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>().OrderBy(m => m.MemberKey).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new List<Member>();
            }
        }
        public static Member CheckIfMemberExist(Member memberRequest)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var member = db.Fetch<Member>()
                        .SingleOrDefault(m => m.Surname.ToLower().Equals(memberRequest.Surname.ToLower())
                        && m.FirstName.ToLower().Equals(memberRequest.FirstName.ToLower()));
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
               LogService.Log(ex.Message);
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
               LogService.Log(ex.Message);
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
               LogService.Log(ex.Message);
                return "Error occured while trying to delete record";
            }
        }
        public static List<Member> GetMembersPendingApproval()
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>().Where(m => m.ApprovedFlag == "N")
                        .OrderBy(m => m.MemberKey)
                        .ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new List<Member>();
            }
        }

        public static List<Member> GetMembersBySearchQuery(string searchQuery)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>()
                        .Where(m => m.FirstName == searchQuery || m.Surname == searchQuery)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new List<Member>();
            }
        }

        public static List<Member> GetMembersByBirthday(string dateOfBirth)
        {
            try
            {
                DateTime.TryParse(dateOfBirth, out var castDateOfBirth);
                using (var db = GdhoteConnection())
                {
                    var members = db.Fetch<Member>()
                        .Where(m => m.DateOfBirth.Date == castDateOfBirth.Date)
                        .OrderBy(m => m.FirstName).ToList();
                    return members;
                }
            }
            catch (Exception ex)
            {
               LogService.Log(ex.Message);
                return new List<Member>();
            }
        }
    }
}
