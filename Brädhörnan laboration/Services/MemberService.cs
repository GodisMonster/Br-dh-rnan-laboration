using Brädhörnan_laboration.Data;
using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using Microsoft.EntityFrameworkCore;

namespace Brädhörnan_laboration.Services
{
    public class MemberService : IMemberService
    {
        public IEnumerable<Member> GetAllMembers()
        {
            using var context = new AppDbContext();
            return context.Members.ToList();
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            using var context = new AppDbContext();
            return await context.Members.ToListAsync();
        }

        public IEnumerable<Member> GetActiveMembers()
        {
            using var context = new AppDbContext();
            return context.Members
                .Where(m => m.Status == MemberStatusEnum.Active)
                .ToList();
        }

        public IEnumerable<Member> GetMembersSortedByName()
        {
            using var context = new AppDbContext();
            return context.Members
                .OrderBy(m => m.LastName)
                .ThenBy(m => m.FirstName)
                .ToList();
        }

        public Member? GetMemberByNumber(int memberNumber)
        {
            using var context = new AppDbContext();
            return context.Members
                .FirstOrDefault(m => m.MemberNumber == memberNumber);
        }

        public void AddMember(Member member)
        {
            using var context = new AppDbContext();
            context.Members.Add(member);
            context.SaveChanges();
        }

        public void UpdateMember(Member member)
        {
            using var context = new AppDbContext();
            context.Members.Update(member);
            context.SaveChanges();
        }

        public void RemoveMember(Member member)
        {
            using var context = new AppDbContext();
            context.Members.Remove(member);
            context.SaveChanges();
        }

        public int GetNextMemberNumber()
        {
            using var context = new AppDbContext();
            return context.Members.Any()
                ? context.Members.Max(m => m.MemberNumber) + 1
                : 1;
        }
    }
}