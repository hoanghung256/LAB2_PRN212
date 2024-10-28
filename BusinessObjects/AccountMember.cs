using System.ComponentModel.DataAnnotations;

namespace BusinessObjects
{
    public class AccountMember
    {
        [StringLength(20)]
        public string MemberID { get; set; } = string.Empty;
        [StringLength(80)]
        public string MemberPassword { get; set; } = string.Empty;
        [StringLength(80)]
        public string FullName { get; set; } = string.Empty;
        [StringLength(100)]
        public string EmailAddress { get; set; } = string.Empty;
        public int MemberRole { get; set; } = 1;

        public AccountMember(string memberID, string memberPassword, string fullName, string emailAddress, int memberRole)
        {
            MemberID = memberID;
            MemberPassword = memberPassword;
            FullName = fullName;
            EmailAddress = emailAddress;
            MemberRole = memberRole;
        }
    }
}