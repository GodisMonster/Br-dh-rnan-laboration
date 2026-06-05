        using Brädhörnan_laboration.Enum;

        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.Threading.Tasks;

namespace Brädhörnan_laboration.Models
{
    public class GameManager
    {
        private readonly List<Game> _games = new();

        private int _nextGameId = 1;

        public Game AddGame(
         string gameName,
         int minPlayers,
         int maxPlayers,
         int averageGameLength,
         DifficultyLevelEnum difficulty,
         GamegenreEnum genre)

        {
            var game = new Game(
                _nextGameId++,
                gameName,
                minPlayers,
                maxPlayers,
                averageGameLength,
                difficulty,
                genre);

            _games.Add(game);

            return game;
        }
        public IEnumerable<Game> GetAllGames()
        {
            return _games.ToList();
        }
        public Game? GetGameById(int gameId)
        {
            return _games.FirstOrDefault(g => g.GameId == gameId);
        }
        public IEnumerable<Game> GetAvailableGames()
        {
            return _games.Where(g => g.IsAvailableForBooking());
        }

        public bool RemoveGame(int gameId)
        {
            var game = GetGameById(gameId);
            if (game == null)
                return false;

            if (!game.IsAvailableForBooking())
                throw new InvalidOperationException(
                    $"'{game.GameName}' är reserverat för en spelträff och kan inte tas bort.");
           
            return _games.Remove(game);


        }

    }
}          
