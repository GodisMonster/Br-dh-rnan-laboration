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
    public int GameId { get; }

    public string GameName { get; private set; }

    public int MinimumNumberOfPlayer { get; private set; }

    public int MaximumNumberOfPlayer { get; private set; }

    public int AverageGameLength { get; private set; }

    public string GameDescription { get; private set; } = string.Empty;

    public DifficultyLevelEnum DifficultyLevel { get; private set; } // = DifficultyLevelEnum.Easy;

    public GamegenreEnum Gamegenre { get; private set; } = GamegenreEnum.Unknown; // Saknar combobox

    public GameAvailabilityEnum GameAvailability { get; private set; }
        = GameAvailabilityEnum.Available;

    public Game(
        int gameId,
        string gameName,
        int minPlayers,
        int maxPlayers,
        int averageGameLength,
        DifficultyLevelEnum difficulty)
    {
        if (gameId <= 0)
            throw new ArgumentOutOfRangeException(nameof(gameId));

        ValidateGameName(gameName);
        ValidatePlayerCounts(minPlayers, maxPlayers);

        if (averageGameLength <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(averageGameLength), "Speltid måste anges");

        GameId = gameId;
        GameName = gameName.Trim();
        MinimumNumberOfPlayer = minPlayers;
        MaximumNumberOfPlayer = maxPlayers;
        AverageGameLength = averageGameLength;
        DifficultyLevel = difficulty;

    
    }
    private static void ValidateGameName(string gameName)
    {
        if (string.IsNullOrWhiteSpace(gameName))
            throw new ArgumentException(
                "Spelnamn kan inte vara tomt",nameof(gameName));

        gameName = gameName.Trim();

        if (gameName.Length < 2 || gameName.Length > 100)
            throw new ArgumentException(
                "Spelnamn måste vara mellan 2 och 100 tecken");
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

    public void ChangePlayerCount(int minPlayers, int maxPlayers)
    {
        ValidatePlayerCounts(minPlayers, maxPlayers);

        MinimumNumberOfPlayer = minPlayers;
        MaximumNumberOfPlayer = maxPlayers;
    }

    public void ChangeName(string newName)
    {
        ValidateGameName(newName);

        GameName = newName.Trim();
    }

    public void UpdateDescription(string description)
    {
       var trimmed = description?.Trim() ?? string.Empty;

        if (trimmed.Length > 500)
            throw new ArgumentException(
                "Spelbeskrivningen får inte vara längre än 500 tecken");
        GameDescription = trimmed;
    }
    public void UpdateGame(
        string gameName,
        int minPlayers,
        int maxPlayers,
        int averageGameLength,
        DifficultyLevelEnum difficulty)
    {
        GameName = gameName;
        MinimumNumberOfPlayer = minPlayers;
        MaximumNumberOfPlayer = maxPlayers;
        AverageGameLength = averageGameLength; 
        DifficultyLevel = difficulty;
        
    }

    public void SetDifficulty(DifficultyLevelEnum difficulty)
    {
        DifficultyLevel = difficulty;
    }

    public void SetGenre(GamegenreEnum genre)
    {
        Gamegenre = genre;
    }

    public void MarkAsReserved()
    {
        if (GameAvailability == GameAvailabilityEnum.Unavailable)
            throw new InvalidOperationException(
                "Spelet är otillgängligt och kan inte reserveras");

        GameAvailability = GameAvailabilityEnum.Reserved;
    }

    public void MarkAsAvailable()
    {
        GameAvailability = GameAvailabilityEnum.Available;
    }

    public void MarkAsUnavailable()
    {
        GameAvailability = GameAvailabilityEnum.Unavailable;
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
        return $"{GameName} ({MinimumNumberOfPlayer}-{MaximumNumberOfPlayer} spelare, " +
               $"{AverageGameLength} min, {DifficultyLevel}) Spel-ID: {GameId}";
    }
}
