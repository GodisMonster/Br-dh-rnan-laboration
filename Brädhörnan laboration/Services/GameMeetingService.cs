using Brädhörnan_laboration.Data;
using Brädhörnan_laboration.Models;
using Microsoft.EntityFrameworkCore;

namespace Brädhörnan_laboration.Services
{
    public class GameMeetingService : IGameMeetingService
    {
        public IEnumerable<GameMeeting> GetAllMeetings()
        {
            using var context = new AppDbContext();
            return context.GameMeetings
                .Include(m => m.Participants)
                .Include(m => m.PlannedGames)
                .Include(m => m.Responsible)
                .ToList();
        }

        public GameMeeting? GetMeetingById(int meetingId)
        {
            using var context = new AppDbContext();
            return context.GameMeetings
                .Include(m => m.Participants)
                .Include(m => m.PlannedGames)
                .Include(m => m.Responsible)
                .FirstOrDefault(m => m.GameMeetingId == meetingId);
        }

        public void AddMeeting(GameMeeting meeting)
        {
            using var context = new AppDbContext();
            context.GameMeetings.Add(meeting);
            context.SaveChanges();
        }

        public void UpdateMeeting(GameMeeting meeting)
        {
            using var context = new AppDbContext();
            context.GameMeetings.Update(meeting);
            context.SaveChanges();
        }

        public void RemoveMeeting(GameMeeting meeting)
        {
            using var context = new AppDbContext();
            context.GameMeetings.Remove(meeting);
            context.SaveChanges();
        }

        public int GetNextMeetingId()
        {
            using var context = new AppDbContext();
            return context.GameMeetings.Any()
                ? context.GameMeetings.Max(m => m.GameMeetingId) + 1
                : 1;
        }
    }
}