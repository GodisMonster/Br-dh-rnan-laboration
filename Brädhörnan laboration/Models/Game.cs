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

    public DifficultyLevelEnum DifficultyLevel { get; private set; } = DifficultyLevelEnum.Easy;

    public GamegenreEnum Gamegenre { get; private set; }

    public GameAvailabilityEnum GameAvailability { get; private set; }
        = GameAvailabilityEnum.Available;

    public Game(
        int gameId,
        string gameName,
        int minPlayers,
        int maxPlayers,
        int averageGameLength)
    {
        if (gameId <= 0)
            throw new ArgumentOutOfRangeException(nameof(gameId));

        ValidateGameName(gameName);
        ValidatePlayerCounts(minPlayers, maxPlayers);

        if (averageGameLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(averageGameLength));

        GameId = gameId;
        GameName = gameName.Trim();
        MinimumNumberOfPlayer = minPlayers;
        MaximumNumberOfPlayer = maxPlayers;
        AverageGameLength = averageGameLength;
    }

    private static void ValidateGameName(string gameName)
    {
        if (string.IsNullOrWhiteSpace(gameName))
            throw new ArgumentException("Spelnamn kan inte vara tomt");

        gameName = gameName.Trim();

        if (gameName.Length < 2 || gameName.Length > 100)
            throw new ArgumentException("Spelnamn måste vara mellan 2 och 100 tecken");
    }

    private static void ValidatePlayerCounts(int min, int max)
    {
        if (min < 1)
            throw new ArgumentOutOfRangeException(nameof(min));

        if (min > max)
            throw new ArgumentOutOfRangeException(nameof(max));

        if (max > 16)
            throw new ArgumentOutOfRangeException(nameof(max));
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
        GameDescription = description?.Trim() ?? string.Empty;
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
               $"{AverageGameLength} min, {DifficultyLevel})";
    }
}
