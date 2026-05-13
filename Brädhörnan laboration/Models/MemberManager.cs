using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Brädhörnan_laboration.Services;

public class MemberManager
{
    private List<Member> _members = new List<Member>();
    private int _nextMemberNumber = 1;


    public Member RegisterNewMember(string firstName, string lastName, string email, string phone = "")
    {

        int memberNumber = _nextMemberNumber++;


        var member = new Member(memberNumber, firstName, lastName, email, phone);
        _members.Add(member);

        return member;
    }

    public IEnumerable<Member> GetAllMembers()
    {
        return _members.ToList();
    }


    public IEnumerable<Member> GetActiveMembers()
    {
        return _members.Where(m => m.Status == MemberStatusEnum.Active);
    }


    public IEnumerable<Member> GetMembersSortedByName()
    {
        return _members.OrderBy(m => m.LastName).ThenBy(m => m.FirstName);
    }


    public IEnumerable<IGrouping<MemberRoleEnum, Member>> GetMembersByRole()
    {
        return _members.GroupBy(m => m.Role);
    }


    public Member? GetMemberByNumber(int memberNumber)
    {
        return _members.FirstOrDefault(m => m.MemberNumber == memberNumber);
    }

    public bool RemoveMember(int memberNumber)
    {
        var member = GetMemberByNumber(memberNumber);
        if (member != null)
        {
            return _members.Remove(member);
        }
        return false;
    }
}


