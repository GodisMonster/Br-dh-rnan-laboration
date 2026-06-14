using Brädhörnan_laboration.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Brädhörnan_laboration.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public GameViewModel GameVM { get; }
        public MemberViewModel MemberVM { get; }
        public GameMeetingViewModel GameMeetingVM { get; }

        public MainViewModel()
        {
            var gameService = new GameService();
            var memberService = new MemberService();
            var meetingService = new GameMeetingService();

            GameVM = new GameViewModel(gameService);
            MemberVM = new MemberViewModel(memberService);
            GameMeetingVM = new GameMeetingViewModel(meetingService, memberService, gameService);

            MemberVM.DataChanged += () => GameMeetingVM.ReloadMembers();
            GameVM.DataChanged += () => GameMeetingVM.ReloadGames();
        }
    }
}