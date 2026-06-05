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
    private readonly List<Member> _participants = new();

    private readonly List<Game> _plannedGames = new();
    public int GameMeetingId { get; }
    public DateTime DateAndTime { get; private set; }
    public string Location { get; private set; } = "";
    public int MaximumNumberOfParticipants { get; private set; }
    public Member? Responsible { get; private set; }
    public EventTypeEnum EventType { get; private set; }
    // public string Information { get; private set; } = ""; // Ej implementerad 

    public IReadOnlyCollection<Game> PlannedGames => _plannedGames.AsReadOnly();
    public IReadOnlyCollection<Member> Participants => _participants.AsReadOnly();
    public bool IsFull 
        => Participants.Count >= MaximumNumberOfParticipants;
    public int AvailableSpots
        => MaximumNumberOfParticipants - Participants.Count;
    public GameMeeting(
        int gameMeetingId,
        DateTime dateAndTime,
        string location,
        int maximumNumberOfParticipants,
        EventTypeEnum eventType) 
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException(
                "Mötesplats måste anges");

        if (maximumNumberOfParticipants < 1)
            throw new ArgumentException(
                "Måste vara minst en deltagare");

        if (dateAndTime < DateTime.Now)
            throw new ArgumentException(
                "Kan inte skapa möten bakåt i tiden");

        if (gameMeetingId < 1)
            throw new ArgumentOutOfRangeException(
                "Ogiltigt mötes-ID");

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
        if (IsFull)
            throw new InvalidOperationException(
                "Träffen är fullbokad.");
        if (IsRegistered(member))
            throw new InvalidOperationException(
                "Medlemmen är redan registrerad.");

        _participants.Add(member);
    }
    public void RemoveParticipant(Member member)
    {
        if (!_participants.Remove(member))
            throw new InvalidOperationException(
                "Medlemmen är inte anmäld till träffen.");
    }
    //public int GetParticipantCount() // Redundant?
    //{
    //    return _participants.Count;
    //}

    //public IEnumerable<string> GetParticipantNames() // Ej implementerad?
    //{
    //        return _participants.Select(
    //            m => $"{m.FirstName} {m.LastName}");    
    //}
    public void AddPlannedGame(Game game)
    {
        if (!game.IsAvailableForBooking())
            throw new InvalidOperationException(
                $"Spelet '{game.GameName}' är inte tillgänglig för bokning.");

        if (_plannedGames.Any(g => g.GameId == game.GameId))
            throw new InvalidOperationException(
                "Spelet är redan reserverat för denna träff.");

        game.MarkAsReserved();

        _plannedGames.Add(game);
    }
    public void RemovePlannedGame(Game game)
    {
        var plannedGame = _plannedGames.FirstOrDefault(g =>  g.GameId == game.GameId);
        if (plannedGame == null)
            throw new InvalidOperationException(
                "Spelet är inte reserverat för denna träff.");

        plannedGame.MarkAsAvailable();

        _plannedGames.Remove(plannedGame); 
    }  
    public bool IsRegistered(Member member)
    {
        return Participants.Any(
            m => m.MemberNumber == member.MemberNumber);
    }

    public override string ToString()
    {
     return $"{DateAndTime:yyyy-MM-dd HH:mm} - " +
            $"Plats: {Location} - " +
            $"Eventtyp: {EventType} - "+
            $"({Participants.Count}/{MaximumNumberOfParticipants} deltagare) - " +
            $"Ansvarig: {(Responsible != null ? Responsible.FirstName : "Ingen ansvarig")}";    
    }
}




