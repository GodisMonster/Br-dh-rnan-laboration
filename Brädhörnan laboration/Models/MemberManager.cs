using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Brädspelsföreningen.Services;

public class MemberManager
{
    private List<Member> _members = new List<Member>();
    private int _nextMemberNumber = 1;

    // Use Case 1: Registrera ny medlem
    public Member RegisterNewMember(string firstName, string lastName, string email, string phone = "")
    {
        // Steg 3: System genererar medlemsnummer
        int memberNumber = _nextMemberNumber++;

        // Steg 2 & 4: Skapa och spara medlem
        var member = new Member(memberNumber, firstName, lastName, email, phone);
        _members.Add(member);

        return member;
    }

    // Steg 5: Visa i medlemslistan
    public IEnumerable<Member> GetAllMembers()
    {
        return _members.ToList(); // Returnera kopia
    }

    // LINQ - Filtrering (KRAV i labben)
    public IEnumerable<Member> GetActiveMembers()
    {
        return _members.Where(m => m.Status == MemberStatusEnum.Active);
    }

    // LINQ - Sortering (KRAV i labben)
    public IEnumerable<Member> GetMembersSortedByName()
    {
        return _members.OrderBy(m => m.LastName).ThenBy(m => m.FirstName);
    }

    // LINQ - Gruppering (KRAV i labben)
    public IEnumerable<IGrouping<MemberRoleEnum, Member>> GetMembersByRole()
    {
        return _members.GroupBy(m => m.Role);
    }

    // Hitta medlem via nummer
    public Member? GetMemberByNumber(int memberNumber)
    {
        return _members.FirstOrDefault(m => m.MemberNumber == memberNumber);
    }

    // Ta bort medlem
    public bool RemoveMember(int memberNumber)
    {
        var member = GetMemberByNumber(memberNumber);
        if (member != null)
        {
            return _members.Remove(member);
        }
        return false;
    }

    // Uppdatera status
    public void UpdateMemberStatus(int memberNumber, MemberStatusEnum newStatus)
    {
        var member = GetMemberByNumber(memberNumber);
        if (member == null)
            throw new InvalidOperationException("Medlem hittades inte");

        member.Status = newStatus;
    }
}