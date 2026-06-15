using Brädhörnan_laboration.Enum;
using System.ComponentModel.DataAnnotations;

namespace Brädhörnan_laboration.Models;

public class Game
{
    private Game()
    {

    }
    [Key]
    public int GameId { get; set; }

    public string GameName { get; private set; } = null!;

    public int MinimumNumberOfPlayer { get; private set; }

    public int MaximumNumberOfPlayer { get; private set; }

    public int AverageGameLength { get; private set; }

    public string GameDescription { get; private set; } = string.Empty;

    public DifficultyLevelEnum DifficultyLevel { get; private set; } 

    public GamegenreEnum Gamegenre { get; private set; } 

    public GameAvailabilityEnum GameAvailability { get; private set; }
        = GameAvailabilityEnum.Available;

    public Game(
        int gameId,
        string gameName,
        int minPlayers,
        int maxPlayers,
        int averageGameLength,
        DifficultyLevelEnum difficulty,
        GamegenreEnum genre)
    {
      //  if (gameId <= 0)
           // throw new ArgumentOutOfRangeException(nameof(gameId));
 
        ValidatePlayerCounts(minPlayers, maxPlayers);

        if (averageGameLength <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(averageGameLength), "Speltid måste anges");

        GameId = gameId;
        GameName = ValidateGameName(gameName);
        MinimumNumberOfPlayer = minPlayers;
        MaximumNumberOfPlayer = maxPlayers;
        AverageGameLength = averageGameLength;
        DifficultyLevel = difficulty;
        Gamegenre = genre;
    }
    private static string ValidateGameName(string gameName)
    {
        if (string.IsNullOrWhiteSpace(gameName))
            throw new ArgumentException(
                "Spelnamn kan inte vara tomt",nameof(gameName));

        gameName = gameName.Trim();

        if (gameName.Length < 2 || gameName.Length > 100)
            throw new ArgumentException( 
                "Spelnamn måste vara mellan 2 och 100 tecken");
        return gameName;
    }
    private static void ValidatePlayerCounts(int min, int max)
    {
        if (min < 1)
            throw new ArgumentOutOfRangeException(nameof(min), "Spel måste ha minst 1 spelare.");

        if (min > max)
            throw new ArgumentOutOfRangeException(nameof(max), "Ett spel kan inte ha lägre maxantal spelare än minsta antal");

        if (max > 16)
            throw new ArgumentOutOfRangeException(nameof(max), "Orimligt många spelare.");
    }
    public void UpdateGame(
        string gameName,
        int minPlayers,
        int maxPlayers,
        int averageGameLength,
        DifficultyLevelEnum difficulty,
        GamegenreEnum genre)
    {
        ValidatePlayerCounts(minPlayers, maxPlayers);

        if (averageGameLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(averageGameLength), "Speltid måste anges");

        GameName = ValidateGameName(gameName);
        MinimumNumberOfPlayer = minPlayers;
        MaximumNumberOfPlayer = maxPlayers;
        AverageGameLength = averageGameLength; 
        DifficultyLevel = difficulty;
        Gamegenre = genre;
        
    }
  
    public void MarkAsReserved()
    {
        if (GameAvailability != GameAvailabilityEnum.Available)
            throw new InvalidOperationException(
                "Spelet är otillgängligt och kan inte reserveras");

        GameAvailability = GameAvailabilityEnum.Reserved;
    }

    public void MarkAsAvailable()
    {
        GameAvailability = GameAvailabilityEnum.Available;
    }
    public bool IsAvailableForBooking()
    {
        return GameAvailability == GameAvailabilityEnum.Available;
    }

    public override string ToString()
    {
        return $"{GameName} ({MinimumNumberOfPlayer}-{MaximumNumberOfPlayer} spelare, " +
               $"{AverageGameLength} min, {DifficultyLevel}) Genre:{Gamegenre} - Spel-ID: {GameId}";
    }
}
