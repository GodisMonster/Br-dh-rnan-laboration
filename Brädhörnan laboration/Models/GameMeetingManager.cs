using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Brädhörnan_laboration.Services;

public class GameMeetingManager
{
    private readonly List<GameMeeting> _meetings = new();

    private int _nextMeetingId = 1;

    public GameMeeting CreateGameMeeting(
        DateTime dateAndTime,
        string location,
        int maxParticipants, EventTypeEnum eventType)
    { 
        var meeting = new GameMeeting(
            _nextMeetingId++,
            dateAndTime,
            location,
            maxParticipants,
            eventType);

        _meetings.Add(meeting);

        return meeting;
    }
    public IEnumerable<GameMeeting> GetAllMeetings() 
    {
        return _meetings.ToList();
    }
    public (bool success, string message) RegisterParticipant(
        int meetingId,
        Member member)
    {
        var meeting = GetMeetingById(meetingId);

        if (meeting == null)
            return (false, "Spelträff hittades inte");

        try
        {   
            meeting.AddParticipant(member);

            return (true, "Anmälan genomförd");
        }
        catch (InvalidOperationException ex)
        {
            return (false, $"Registrering misslyckades: {ex.Message}");
        }
    }
    public (bool success, string message) UnregisterParticipant(
        int meetingId,
        Member member)
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
            return (false,$"Avregistrering misslyckades: {ex.Message}");
        }
    }
    public GameMeeting? GetMeetingById(int meetingId)
    {
        return _meetings.FirstOrDefault(
            m => m.GameMeetingId == meetingId);
    } 
    public (bool success, string message) AddGameToMeeting(
     int meetingId,
     Game game)
    {
        var meeting = GetMeetingById(meetingId);

        if (meeting == null)
            return (false, "Spelträff hittades inte");

        try
        {
            meeting.AddPlannedGame(game);

            return (true, "Spelet reserverades");
        }
        catch (InvalidOperationException ex)
        {
            return (false, ex.Message);
        }
    }
    public (bool success, string message) RemoveGameFromMeeting(
    int meetingId,
    Game game)
    {
        var meeting = GetMeetingById(meetingId);
        if (meeting == null)
            return (false, "Spelträff hittades inte");

        try
        {
            meeting.RemovePlannedGame(game);
        
            return (true, "Spelet frigjordes");
        }
        catch (InvalidOperationException)
        {
            return (false, "Misslyckad frigörning");
        }
    }
    public IEnumerable<GameMeeting> GetUpcomingMeetings()
    {
        return _meetings
            .Where(m => m.DateAndTime > DateTime.Now)
            .OrderBy(m => m.DateAndTime);
    }
    public IEnumerable<GameMeeting> GetMeetingsWithAvailableSpots()
    {
        return _meetings
            .Where(m => !m.IsFull &&
            m.DateAndTime > DateTime.Now);
    } 
    public IEnumerable<IGrouping<EventTypeEnum, GameMeeting>> GetMeetingsByType()
    {
        return _meetings.GroupBy(m => m.EventType);
    }
    public bool RemoveMeeting(int meetingId)
    {
        var meeting = GetMeetingById(meetingId);

        if(meeting == null)
            return false;

        if (meeting.Participants.Any())
            return false;
        var gamesToRemove = meeting.PlannedGames.ToList();
        foreach (var game in gamesToRemove)
        {
            meeting.RemovePlannedGame(game);
        }
        return _meetings.Remove(meeting);


    }
}
