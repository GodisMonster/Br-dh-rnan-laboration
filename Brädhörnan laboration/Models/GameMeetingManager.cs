using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Brädhörnan_laboration.Services;

public class GameMeetingManager
{
    private List<GameMeeting> _meetings = new();
    private int _nextMeetingId = 1;

    // Use Case 2 för att skapa ny spelträff
    public GameMeeting CreateGameMeeting(DateTime dateAndTime, string location,
                                          int maxParticipants, EventTypeEnum eventType)
    {
        // Steg 3-4: System skapar träff (validering i konstruktorn)
        int meetingId = _nextMeetingId++;
        var meeting = new GameMeeting(meetingId, dateAndTime, location, maxParticipants, eventType);

        _meetings.Add(meeting);
        return meeting;
    }

    // Steg 5: Visa i kalendern
    public IEnumerable<GameMeeting> GetAllMeetings()
    {
        return _meetings.ToList();
    }

    // Use Case 3: Anmäla till spelträff (MED felhantering)
    public (bool success, string message) RegisterParticipant(int meetingId, Member member)
    {
        var meeting = GetMeetingById(meetingId);
        if (meeting == null)
            return (false, "Spelträff hittades inte");

        try
        {
            // Steg 2-4: Kontroller och registrering
            meeting.AddParticipant(member);
            return (true, "Anmälan genomförd");
        }
        catch (InvalidOperationException ex)
        {
            // Användarvänligt felmeddelande istället för krasch
            return (false, ex.Message);
        }
    }

    // Avanmälan
    public (bool success, string message) UnregisterParticipant(int meetingId, Member member)
    {
        var meeting = GetMeetingById(meetingId);
        if (meeting == null)
            return (false, "Spelträff hittades inte");

        try
        {
            meeting.RemoveParticipant(member);
            return (true, "Avanmälan genomförd");
        }
        catch (InvalidOperationException ex)
        {
            return (false, ex.Message);
        }
    }

    // LINQ - Hitta träff
    public GameMeeting? GetMeetingById(int meetingId)
    {
        return _meetings.FirstOrDefault(m => m.GameMeetingId == meetingId);
    }

    // LINQ - Filtrering: Kommande träffar
    public IEnumerable<GameMeeting> GetUpcomingMeetings()
    {
        return _meetings
            .Where(m => m.DateAndTime > DateTime.Now)
            .OrderBy(m => m.DateAndTime);
    }

    // LINQ - Filtrering: Träffar med lediga platser
    public IEnumerable<GameMeeting> GetMeetingsWithAvailableSpots()
    {
        return _meetings.Where(m => !m.IsFull && m.DateAndTime > DateTime.Now);
    }

    // LINQ - Gruppering: Träffar per typ
    public IEnumerable<IGrouping<EventTypeEnum, GameMeeting>> GetMeetingsByType()
    {
        return _meetings.GroupBy(m => m.EventType);
    }

    // Ta bort träff
    public bool RemoveMeeting(int meetingId)
    {
        var meeting = GetMeetingById(meetingId);
        if (meeting != null)
        {
            return _meetings.Remove(meeting);
        }
        return false;
    }
}