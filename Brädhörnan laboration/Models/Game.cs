using Brädhörnan_laboration.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Brädhörnan_laboration.Models;

public class Game
{
    public int GameId { get; set; }
    public string GameName { get; set; }
    public int MinimumNumberOfPlayer { get; set; }
    public int MaximumNumberOfPlayer { get; set; }
    public int AverageGameLength { get; set; }
    public string GameDescription { get; set; }

    public DifficultyLevelEnum DifficultyLevel { get; set; }
    public GamegenreEnum Gamegenre { get; set; }
    public GameAvailabilityEnum GameAvailability { get; set; }

    public Game(int gameId, string gameName, int minimumNumberOfPlayers, int maximumNumberOfPlayers)
    {
        ValidateGameName(gameName);
        ValidateGameNumbers(minimumNumberOfPlayers, maximumNumberOfPlayers);


        if (gameId <= 0)
            throw new ArgumentOutOfRangeException("Spel-ID måste vara positivt");

        GameId=gameId;
        GameName=gameName;
        MinimumNumberOfPlayer=minimumNumberOfPlayers;
        MaximumNumberOfPlayer=maximumNumberOfPlayers;
    }

    private static void ValidateGameName(string gameName)
    {
        if (string.IsNullOrEmpty(gameName))
            throw new ArgumentException("Spelnamn kan inte vara tomt");

        gameName = gameName.Trim();
        if (gameName.Length < 2 || gameName.Length > 100)
            throw new ArgumentException("Spelnamn måste vara mellan 2 och 100 tecken");
    }

    private static void ValidateGameNumbers(int min, int max)
    {
        if (min < 1)
            throw new ArgumentOutOfRangeException("Minsta antal spelare måste vara minst 1");

        if (min  > max) throw new ArgumentOutOfRangeException("Max antal spelare kan inte vara mindre än minsta antal");

        if (max > 16) throw new ArgumentOutOfRangeException("Max antal spelare verkar orimligt högt");
    }
    public void MarkAsReserved()
    {
        if (GameAvailability == GameAvailabilityEnum.Unavailable)
            throw new InvalidOperationException("Spelet är otillgängligt och kan inte reserveras");
    }
    public void MarkAsAvailable()
    {
        GameAvailability = GameAvailabilityEnum.Available;
    }
    public void MarkAsUnavailable()
    {
        GameAvailability =GameAvailabilityEnum.Unavailable;
    }
    public bool IsSuitableForPlayerCount(int numberOfPlayers)
    {
        return numberOfPlayers >= MinimumNumberOfPlayer
        && numberOfPlayers <= MaximumNumberOfPlayer;
    }
    public bool IsAvailableForBooking()
    {
        return GameAvailability == GameAvailabilityEnum.Available;
    }
    public override string ToString()
    {
        return $"{GameName} ({MinimumNumberOfPlayer}-{MaximumNumberOfPlayer} spelare, " + $"{AverageGameLength} min,  {DifficultyLevel}";
    }
}




