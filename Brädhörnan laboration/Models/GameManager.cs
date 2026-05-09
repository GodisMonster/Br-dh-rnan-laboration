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
        private List<Game> _games = new List<Game>();
        private int _nextGameId = 1;

        public Game AddGame(string gameName, int minPlayers, int maxPlayers)
        {
            int gameID = _nextGameId++;
            var game = new Game(gameID, gameName, minPlayers, maxPlayers);
            _games.Add(game);
            return game;
        }
        public IEnumerable<Game> GetAllGames()
        {
            return _games.ToList();
        }
        public IEnumerable<Game> GetAvailableGames()
        {
            return _games.Where(g => g.GameAvailability == GameAvailabilityEnum.Available);
        }
        public IEnumerable<Game> GetGamesForPlayerCount(int numberOfPlayers)
        {
            return _games.Where(g => g.IsSuitableForPlayerCount(numberOfPlayers));
        }
        public IEnumerable<Game> GetGamesSortedByName()
        {
            return _games.OrderBy(g => g.GameName);
        }
        public IEnumerable<IGrouping<GamegenreEnum, Game>> GetGamesByGenre()
        {
            return _games.GroupBy(g => g.Gamegenre);
        }
        public Game? GetGameById(int gameID)
        {
            return _games.FirstOrDefault(g => g.GameId == gameID);
        }
        public bool RemoveGame(int gameID)
        {
            var game = GetGameById(gameID);
            if (game != null)
            {
                return _games.Remove(game);
            }
            return false;
        }

    }
}
