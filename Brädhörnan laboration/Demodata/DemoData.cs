using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Services;

namespace Brädhörnan_laboration.Data;

public static class DemoData
{
    public static MemberManager MembersDemoData()
    {
        var memberManager = new MemberManager();

        
        var anna = memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Anna",
            "Andersson",
            "anna.andersson@email.com",
            "070-123 45 67",
            MemberStatusEnum.Active,
            MemberRoleEnum.Admin);

        var erik = memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Erik",
            "Eriksson",
            "erik.eriksson@email.com",
            "070-234 56 78",
            MemberStatusEnum.Active,
            MemberRoleEnum.Organizer);

        var maria = memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Maria",
            "Svensson",
            "maria.svensson@email.com",
            "070-345 67 89",
            MemberStatusEnum.Active,
            MemberRoleEnum.Member);

        var johan = memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Johan",
            "Johansson",
            "johan.johansson@email.com",
            "070-456 78 90",
            MemberStatusEnum.Active,
            MemberRoleEnum.Member);

        var karin = memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Karin",
            "Karlsson",
            "karin.karlsson@email.com",
            "070-567 89 01",
            MemberStatusEnum.Inactive,
            MemberRoleEnum.Member);

        var lars = memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Lars",
            "Larsson",
            "lars.larsson@email.com",
            "",
            MemberStatusEnum.Active,
            MemberRoleEnum.Member);

        return memberManager;
    }
      public static GameManager GameDemoData()
    {
        var gameManager = new GameManager();

        var blackjack = gameManager.AddGame("Black Jack", 2, 8, 60, DifficultyLevelEnum.Intermediate, GamegenreEnum.Strategy);

        var monopol = gameManager.AddGame("Monopol",2,5,90, DifficultyLevelEnum.Intermediate,GamegenreEnum.Strategy);

        var Tekken6 = gameManager.AddGame("Tekken 6", 1, 2, 20, DifficultyLevelEnum.Intermediate, GamegenreEnum.Unknown);

        var Schack = gameManager.AddGame("Schack", 2, 2, 30, DifficultyLevelEnum.Intermediate, GamegenreEnum.Classic);

        return gameManager;
    }
    public static GameMeetingManager GameMeetingDemoData(MemberManager memberManager, GameManager gameMananger)
    {
        var meetingManager = new GameMeetingManager();

        var members = memberManager.GetAllMembers().ToList();
        var games = gameMananger.GetAllGames().ToList();

     ;
        var anna = members[0];
        var erik = members[1];
        var maria = members[2];
        var johan = members[3];

        var blackjack = games[0];
        var monopol = games[1];
        var schack = games[3];

        var spelkvall = meetingManager.CreateGameMeeting(
         
            DateTime.Now.AddDays(7),
            "Föreningslokalen",
            6,
            EventTypeEnum.Opening_evening);

        spelkvall.AddParticipant(anna);
        spelkvall.AddParticipant(erik);
        spelkvall.AddParticipant(maria);
        spelkvall.AddPlannedGame(blackjack);

        var turneringen = meetingManager.CreateGameMeeting(
      
            DateTime.Now.AddDays(14),
            "Stora salen",
            4,
            EventTypeEnum.Tournament);

        turneringen.AddParticipant(erik);
        turneringen.AddParticipant(johan);
        turneringen.AddPlannedGame(schack);

        return meetingManager;
    }



}

        

        

      

