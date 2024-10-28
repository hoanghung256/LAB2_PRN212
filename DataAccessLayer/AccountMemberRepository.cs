using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountMemberRepository : IAccountMemberRepository
    {
        public AccountMember? GetMember(string memberId, string password)
        {
            using var dbConext = new ProductStoreDbContext();

            return dbConext.AccountMembers.FirstOrDefault(a => (
                a.MemberID == memberId && a.MemberPassword == password
            ));
        }
    }
}
