using Brädhörnan_laboration.Data;
using System.Windows;

namespace Brädhörnan_laboration
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            if (!context.Members.Any() && !context.Games.Any())
            {
                var memberManager = DemoData.MembersDemoData();
                var gameManager = DemoData.GameDemoData();
                var meetingManager = DemoData.GameMeetingDemoData(memberManager, gameManager);

                foreach (var member in memberManager.GetAllMembers())
                    context.Members.Add(member);

                foreach (var game in gameManager.GetAllGames())
                    context.Games.Add(game);

                context.SaveChanges();

                foreach (var meeting in meetingManager.GetAllMeetings())
                    context.GameMeetings.Add(meeting);

                context.SaveChanges();
            }
        }
    }
}