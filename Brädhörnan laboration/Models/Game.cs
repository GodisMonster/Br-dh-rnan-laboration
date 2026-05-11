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
    public int AverageGameLength { get; init; }
    public string GameDescription { get; set; } = string.Empty; // Default

    public DifficultyLevelEnum DifficultyLevel { get; set; } = DifficultyLevelEnum.Easy; // Default
    public GamegenreEnum Gamegenre { get; set; }
    public GameAvailabilityEnum GameAvailability { get; set; } = GameAvailabilityEnum.Available; // Default

    public Game(int gameId, string gameName, int minPlayers, int maxPlayers)
    {
 

        if (gameId <= 0)
            throw new ArgumentOutOfRangeException(nameof(gameId), "Spel-ID måste vara positivt");

        if (string.IsNullOrEmpty(gameName))
            throw new ArgumentException("Spelnamn kan inte vara tomt",
                nameof(GameName));

        gameName = gameName.Trim();
        if (gameName.Length < 2 || gameName.Length > 100)
            throw new ArgumentException("Spelnamn måste vara mellan 2 och 100 tecken");

        ValidateGameNumbers(minPlayers, maxPlayers);

        GameId=gameId;
        GameName=gameName;
        MinimumNumberOfPlayer=minPlayers;
        MaximumNumberOfPlayer=maxPlayers;
    }

 

    private static void ValidateGameNumbers(int min, int max)
    {
        if (min < 1)
            throw new ArgumentOutOfRangeException(nameof(min), "Minsta antal spelare måste vara minst 1");

        if (min  > max) throw new ArgumentOutOfRangeException(nameof(max), "Max antal spelare kan inte vara mindre än minsta antal");

        if (max > 16) throw new ArgumentOutOfRangeException(nameof(max), "Max antal spelare verkar orimligt högt");
    }
    public void MarkAsReserved()
    {
        if (GameAvailability == GameAvailabilityEnum.Unavailable)
            throw new InvalidOperationException("Spelet är otillgängligt och kan inte reserveras");
        GameAvailability = GameAvailabilityEnum.Reserved;
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
    public override string ToString() // Kolla om jag behöver lägga in fler parametrar
    {
        return $"{GameName} ({MinimumNumberOfPlayer}-{MaximumNumberOfPlayer} spelare, " + $"{AverageGameLength} min,  {DifficultyLevel})";
    }
}




