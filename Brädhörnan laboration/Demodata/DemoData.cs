using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Services;

namespace Brädhörnan_laboration.Data;

public static class DemoData
{
    public static MemberManager MembersDemoData()
    {
        var memberManager = new MemberManager();

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Anna", "Andersson", "anna.andersson@email.com",
            "070-123 45 67", MemberStatusEnum.Active, MemberRoleEnum.Admin);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Erik", "Eriksson", "erik.eriksson@email.com",
            "070-234 56 78", MemberStatusEnum.Active, MemberRoleEnum.Organizer);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Maria", "Svensson", "maria.svensson@email.com",
            "070-345 67 89", MemberStatusEnum.Active, MemberRoleEnum.Member);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Johan", "Johansson", "johan.johansson@email.com",
            "070-456 78 90", MemberStatusEnum.Active, MemberRoleEnum.Member);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Karin", "Karlsson", "karin.karlsson@email.com",
            "070-567 89 01", MemberStatusEnum.Inactive, MemberRoleEnum.Member);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Lars", "Larsson", "lars.larsson@email.com",
            "", MemberStatusEnum.Active, MemberRoleEnum.Member);

        // Nya medlemmar
        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Sofia", "Nilsson", "sofia.nilsson@email.com",
            "070-678 90 12", MemberStatusEnum.Active, MemberRoleEnum.Organizer);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Peter", "Pettersson", "peter.pettersson@email.com",
            "070-789 01 23", MemberStatusEnum.Active, MemberRoleEnum.Member);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Lena", "Lindqvist", "lena.lindqvist@email.com",
            "070-890 12 34", MemberStatusEnum.Inactive, MemberRoleEnum.Member);

        memberManager.RegisterNewMember(MemberRoleEnum.Admin,
            "Mikael", "Magnusson", "mikael.magnusson@email.com",
            "", MemberStatusEnum.Active, MemberRoleEnum.Member);

        return memberManager;
    }

    public static GameManager GameDemoData()
    {
        var gameManager = new GameManager();

        gameManager.AddGame("Black Jack", 2, 8, 60, DifficultyLevelEnum.Intermediate, GamegenreEnum.Strategy);
        gameManager.AddGame("Monopol", 2, 5, 90, DifficultyLevelEnum.Intermediate, GamegenreEnum.Strategy);
        gameManager.AddGame("Tekken 6", 1, 2, 20, DifficultyLevelEnum.Intermediate, GamegenreEnum.Unknown);
        gameManager.AddGame("Schack", 2, 2, 30, DifficultyLevelEnum.Intermediate, GamegenreEnum.Classic);

        // Nya spel
        gameManager.AddGame("Catan", 3, 4, 120, DifficultyLevelEnum.Intermediate, GamegenreEnum.Strategy);
        gameManager.AddGame("Uno", 2, 10, 30, DifficultyLevelEnum.Easy, GamegenreEnum.Classic);
        gameManager.AddGame("Risk", 2, 6, 180, DifficultyLevelEnum.Advanced, GamegenreEnum.Strategy);
        gameManager.AddGame("Dixit", 3, 6, 45, DifficultyLevelEnum.Easy, GamegenreEnum.Unknown);

        return gameManager;
    }

    public static GameMeetingManager GameMeetingDemoData(MemberManager memberManager, GameManager gameManager)
    {
        var meetingManager = new GameMeetingManager();

        var members = memberManager.GetAllMembers().ToList();
        var games = gameManager.GetAllGames().ToList();

        var anna = members[0];
        var erik = members[1];
        var maria = members[2];
        var johan = members[3];
        var sofia = members[6];
        var peter = members[7];
        var mikael = members[9];

        var blackjack = games[0];
        var monopol = games[1];
        var schack = games[3];
        var catan = games[4];
        var uno = games[5];
        var risk = games[6];
        var dixit = games[7];

        // Befintliga möten
        var spelkvall = meetingManager.CreateGameMeeting(
            DateTime.Now.AddDays(7), "Föreningslokalen", 6, EventTypeEnum.Opening_evening);
        spelkvall.AddParticipant(anna);
        spelkvall.AddParticipant(erik);
        spelkvall.AddParticipant(maria);
        spelkvall.AddPlannedGame(blackjack);

        var turneringen = meetingManager.CreateGameMeeting(
            DateTime.Now.AddDays(14), "Stora salen", 4, EventTypeEnum.Tournament);
        turneringen.AddParticipant(erik);
        turneringen.AddParticipant(johan);
        turneringen.AddPlannedGame(schack);

        // Nya möten
        var familjekvall = meetingManager.CreateGameMeeting(
            DateTime.Now.AddDays(3), "Föreningslokalen", 8, EventTypeEnum.Intro_evening);
        familjekvall.AddParticipant(sofia);
        familjekvall.AddParticipant(peter);
        familjekvall.AddParticipant(mikael);
        familjekvall.AddPlannedGame(uno);
        familjekvall.AddPlannedGame(dixit);

        var strategikvall = meetingManager.CreateGameMeeting(
            DateTime.Now.AddDays(21), "Stora salen", 6, EventTypeEnum.Tournament);
        strategikvall.AddParticipant(anna);
        strategikvall.AddParticipant(johan);
        strategikvall.AddParticipant(sofia);
        strategikvall.AddParticipant(peter);
        strategikvall.AddPlannedGame(catan);
        strategikvall.AddPlannedGame(risk);

        var monopolkvall = meetingManager.CreateGameMeeting(
            DateTime.Now.AddDays(10), "Källarlokalen", 5, EventTypeEnum.Opening_evening);
        monopolkvall.AddParticipant(maria);
        monopolkvall.AddParticipant(mikael);
        monopolkvall.AddParticipant(erik);
        monopolkvall.AddPlannedGame(monopol);

        return meetingManager;
    }
}