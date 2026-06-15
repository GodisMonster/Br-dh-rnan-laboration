using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace Brädhörnan_laboration.ViewModels
{
    public partial class GameMeetingViewModel : ObservableObject
    {
        private readonly IGameMeetingService _meetingService;
        private readonly IMemberService _memberService;
        private readonly IGameService _gameService;

        public ObservableCollection<GameMeeting> Meetings { get; } = new();
        public ObservableCollection<Member> AvailableMembers { get; } = new();
        public ObservableCollection<Game> AvailableGames { get; } = new();
        public ObservableCollection<Member> Participants { get; } = new();
        public ObservableCollection<Game> PlannedGames { get; } = new();
        public IEnumerable<EventTypeEnum> EventTypes => System.Enum.GetValues<EventTypeEnum>();

        [ObservableProperty] private GameMeeting? _selectedMeeting;
        [ObservableProperty] private Member? _selectedParticipant;
        [ObservableProperty] private Game? _selectedGame;
        [ObservableProperty] private string _location = "";
        [ObservableProperty] private string _maxParticipants = "";
        [ObservableProperty] private DateTime _meetingDate = DateTime.Now.AddDays(1);
        [ObservableProperty] private string _meetingTime = "";
        [ObservableProperty] private EventTypeEnum _selectedEventType;
        [ObservableProperty] private Member? _responsible;

        public GameMeetingViewModel(IGameMeetingService meetingService, IMemberService memberService, IGameService gameService)
        {
            _meetingService = meetingService;
            _memberService = memberService;
            _gameService = gameService;
            LoadMeetings();
            ReloadMembers();
            ReloadGames();
        }

        private void LoadMeetings()
        {
            Meetings.Clear();
            foreach (var m in _meetingService.GetAllMeetings())
                Meetings.Add(m);
        }

        public void ReloadMembers()
        {
            AvailableMembers.Clear();
            foreach (var m in _memberService.GetAllMembers())
                AvailableMembers.Add(m);
        }

        public void ReloadGames()
        {
            AvailableGames.Clear();
            foreach (var g in _gameService.GetAvailableGames())
                AvailableGames.Add(g);
        }

        partial void OnSelectedMeetingChanged(GameMeeting? value)
        {
            Participants.Clear();
            PlannedGames.Clear();
            if (value == null) return;
            foreach (var p in value.Participants) Participants.Add(p);
            foreach (var g in value.PlannedGames) PlannedGames.Add(g);
            ReloadGames();
            ReloadMembers();
        }

        [RelayCommand]
        private void CreateMeeting()
        {
            try
            {
                if (!int.TryParse(MaxParticipants, out int max)) { MessageBox.Show("Max deltagare måste vara ett nummer."); return; }
                if (!TimeSpan.TryParse(MeetingTime, out TimeSpan time)) { MessageBox.Show("Ange giltig tid, t.ex. 17:45."); return; }

                var dateAndTime = MeetingDate.Date + time;
                var id = _meetingService.GetNextMeetingId();
                var meeting = new GameMeeting(id, dateAndTime, Location, max, SelectedEventType);

                if (Responsible != null)
                    meeting.SetResponsible(Responsible);

                _meetingService.AddMeeting(meeting);
                LoadMeetings();
                Location = "";
                MaxParticipants = "";
                MeetingTime = "";
                MessageBox.Show("Spelträff skapad!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void ClearSelectedMeeting()
        {
            SelectedMeeting = null;
            Participants.Clear();
            PlannedGames.Clear();
            ReloadGames();
            ReloadMembers();
        }

        [RelayCommand]
        private void AddParticipant()
        {
            if (SelectedMeeting == null) { MessageBox.Show("Välj en spelträff."); return; }
            if (SelectedParticipant == null) { MessageBox.Show("Välj en medlem."); return; }
            try
            {
                SelectedMeeting.AddParticipant(SelectedParticipant);
                _meetingService.UpdateMeeting(SelectedMeeting);
                Participants.Clear();
                foreach (var p in SelectedMeeting.Participants) Participants.Add(p);
                LoadMeetings();
                MessageBox.Show("Deltagare tillagd!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void RemoveParticipant()
        {
            if (SelectedMeeting == null) { MessageBox.Show("Välj en spelträff."); return; }
            if (SelectedParticipant == null) { MessageBox.Show("Välj en deltagare."); return; }
            try
            {
                SelectedMeeting.RemoveParticipant(SelectedParticipant);
                _meetingService.UpdateMeeting(SelectedMeeting);
                Participants.Clear();
                foreach (var p in SelectedMeeting.Participants) Participants.Add(p);
                LoadMeetings();
                MessageBox.Show("Deltagare borttagen!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void BookGame()
        {
            if (SelectedMeeting == null) { MessageBox.Show("Välj en spelträff."); return; }
            if (SelectedGame == null) { MessageBox.Show("Välj ett spel."); return; }
            try
            {
                SelectedMeeting.AddPlannedGame(SelectedGame);
                _meetingService.UpdateMeeting(SelectedMeeting);
                PlannedGames.Clear();
                foreach (var g in SelectedMeeting.PlannedGames) PlannedGames.Add(g);
                ReloadGames();
                MessageBox.Show("Spel bokat!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void UnbookGame()
        {
            if (SelectedMeeting == null) { MessageBox.Show("Välj en spelträff."); return; }
            if (SelectedGame == null) { MessageBox.Show("Välj ett spel."); return; }
            try
            {
                SelectedMeeting.RemovePlannedGame(SelectedGame);
                _meetingService.UpdateMeeting(SelectedMeeting);
                PlannedGames.Clear();
                foreach (var g in SelectedMeeting.PlannedGames) PlannedGames.Add(g);
                ReloadGames();
                MessageBox.Show("Spel avbokat!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}