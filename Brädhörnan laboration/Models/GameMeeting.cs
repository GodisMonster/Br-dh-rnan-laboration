using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Brädhörnan_laboration.Models;

public class GameMeeting
{
    public int GameMeetingId { get; set; }
    public DateTime DateAndTime { get; set; }
    public string Location { get; set; } = "";
    public int MaximumNumberOfParticipants { get; set; }
    public Member? Responsible { get; set; }

    public EventTypeEnum EventType { get; set; }

    public string Information { get; set; } = "";

    public List<Member> Participants { get;  set; } = new();

    public List<Game> PlannedGames { get;  set; } = new();

    public GameMeeting(int gameMeetingId, DateTime dateAndTime, string location,
                        int maximumNumberOfParticipants, EventTypeEnum eventType) 
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Mötesplats kan inte vara tomt");

        if (maximumNumberOfParticipants < 1)
            throw new ArgumentException("Måste vara minst en deltagare");

        if (dateAndTime < DateTime.Now)
            throw new ArgumentException("Kan inte skapa möten bakåt i tiden");

        GameMeetingId = gameMeetingId;
        DateAndTime = dateAndTime;
        Location = location;
        MaximumNumberOfParticipants = maximumNumberOfParticipants;
        EventType = eventType;
    }
    public void SetResponsible(Member responsible)
    {
        Responsible = responsible;
    }

    public void AddParticipant(Member member)
    {
        if (Participants.Count >= MaximumNumberOfParticipants)
            throw new InvalidOperationException("Meeting is full");
        if (Participants.Any(m => m.MemberNumber == member.MemberNumber))
            throw new InvalidOperationException("Member already registered");

        Participants.Add(member);
    }

    public void RemoveParticipant(Member member)
    {
        if (!Participants.Remove(member))
            throw new InvalidOperationException("Member not found in participants");
    }
    public int GetParticipantCount()
    {
        return Participants.Count;
    }

    public IEnumerable<string> GetParticipantNames() 
    {
        
        {
            return Participants.Select(m => $"{m.FirstName} {m.LastName}");
        }
    }
    public void ReserveGame(Game game)
    {
        if (!game.IsAvailableForBooking())
            throw new InvalidOperationException($"Spelet '{game.GameName}' är inte tillgänglig för bokning.");

        if (PlannedGames.Any(g => g.GameId == game.GameId))
            throw new InvalidOperationException("Spelet är redan reserverat för denna träff.");
               PlannedGames.Add(game);
    }
    public void UnreserveGame(Game game)
    {
        var plannedGame = PlannedGames.FirstOrDefault(g =>  g.GameId == game.GameId);
        if (plannedGame == null)
            throw new InvalidOperationException("Spelet är inte reserverat för denna träff.");
        PlannedGames.Remove(plannedGame); 
    }
    public void AddPlannedGame(Game game)
    {
        if (PlannedGames.Any(g => g.GameId == game.GameId))
            throw new InvalidOperationException("Spelet är redan planerat för denna träff.");
        PlannedGames.Add(game);
    }
    public void RemovePlannedGame(Game game)
    {
        var plannedGame = PlannedGames.FirstOrDefault(g => g.GameId == game.GameId);
        if (plannedGame != null)
            PlannedGames.Remove(plannedGame);
    }


    public bool IsRegistered(Member member)
    {
        return Participants.Any(m => m.MemberNumber == member.MemberNumber);
    }

    public bool IsFull => Participants.Count >= MaximumNumberOfParticipants;
    public int AvailableSpots => MaximumNumberOfParticipants - Participants.Count;
}



