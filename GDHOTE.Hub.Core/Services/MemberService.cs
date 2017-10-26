using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDHOTE.Hub.Core.Models;

namespace GDHOTE.Hub.Core.Services
{
    public class MemberService : BaseService
    {
        public void Save(Member member)
        {
            try
            {
                using (var db = GdhoteConnection())
                {
                    db.Insert(member);
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
                    var members = db.Query<Member>("select * FROM GDH_Members");
                    return members;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to fetch member");
            }
        }
    }
}
