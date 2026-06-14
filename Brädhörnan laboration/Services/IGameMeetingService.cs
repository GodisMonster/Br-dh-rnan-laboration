using Brädhörnan_laboration.Models;

namespace Brädhörnan_laboration.Services
{
    public interface IGameMeetingService
    {
        IEnumerable<GameMeeting> GetAllMeetings();
        GameMeeting? GetMeetingById(int meetingId);
        void AddMeeting(GameMeeting meeting);
        void UpdateMeeting(GameMeeting meeting);
        void RemoveMeeting(GameMeeting meeting);
        int GetNextMeetingId();
    }
}