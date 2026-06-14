using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;

namespace Brädhörnan_laboration.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAllMembers();
        IEnumerable<Member> GetActiveMembers();
        IEnumerable<Member> GetMembersSortedByName();
        Member? GetMemberByNumber(int memberNumber);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void RemoveMember(Member member);
        int GetNextMemberNumber();
        Task<IEnumerable<Member>> GetAllMembersAsync();
    }
}