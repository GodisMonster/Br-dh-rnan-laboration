using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brädhörnan_laboration.Models;

public class GameMeeting
{
    public int GameMeetingId { get; private set; }
    public DateTime DateAndTime { get; set; }
    public string Location { get; set; } = "";
    public int MaximumNumberOfParticipants { get; private set; }
    public MemberRoleEnum Responsible { get; set; }

    public EventTypeEnum EventType { get; set; }

    public string Information { get; set; } = "";

    public List<Member> Participants { get; private set; } = new();

    public List<Game> PlannedGames { get; private set; } = new();

    public GameMeeting(int gameMeetingId, DateTime dateAndTime, string location,
                        int maximumNumberOfParticipants, EventTypeEnum eventType)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty");

        if (maximumNumberOfParticipants < 1)
            throw new ArgumentException("Must allow at least 1 participant");

        if (dateAndTime < DateTime.Now)
            throw new ArgumentException("Cannot create meeting in the past");

        GameMeetingId = gameMeetingId;
        DateAndTime = dateAndTime;
        Location = location;
        MaximumNumberOfParticipants = maximumNumberOfParticipants;
        EventType = eventType;
    }
    // Metod för att anmäla medlem (validerar att träffen inte är full)
    public void AddParticipant(Member member)
    {
        if (Participants.Count >= MaximumNumberOfParticipants)
            throw new InvalidOperationException("Meeting is full");
        if (Participants.Contains(member))
            throw new InvalidOperationException("Member already registered");

        Participants.Add(member);
    }

    public void RemoveParticipant(Member member)
    {
        if (!Participants.Remove(member))
            throw new InvalidOperationException("Member not found in participants");
    }
    public void NumberOfRegistred(Member member)
    {
        Console.WriteLine(Participants.Count);
    }

    public void FetchMember()
    {
        foreach (Member m in Participants)
        {
            Console.WriteLine($"{m.FirstName}: {m.LastName}");
        }

    }

    public bool IsRegistred(Member member)
    {
        return Participants.Any(m => m.MemberNumber == member.MemberNumber);
    }

    public bool IsFull => Participants.Count >= MaximumNumberOfParticipants;
    public int AvailableSpots => MaximumNumberOfParticipants - Participants.Count;
}



