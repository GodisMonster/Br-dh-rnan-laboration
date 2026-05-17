using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;


namespace Brädhörnan_laboration.Services;

public class MemberManager
{
    private readonly List<Member> _members = new List<Member>(); // varför ska den vara readonly?

    private int _nextMemberNumber = 1;
    public Member RegisterNewMember(
    string firstName,
    string lastName,
    string email,
    string phone,
    MemberStatusEnum status,
    MemberRoleEnum role)
    {
        var member = new Member(
            _nextMemberNumber++,  
            firstName,            
            lastName,             
            email,              
            status,              
            role,                
            phone);               

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


    public Member? GetMemberByMemberNumber(int memberNumber)
    {
        return _members.FirstOrDefault(m => m.MemberNumber == memberNumber);
    }

    public bool RemoveMember(int memberNumber)
    {
        var member = GetMemberByMemberNumber(memberNumber);
        if (member != null)
        {
            return _members.Remove(member);
        }
        return false;
    }

}


