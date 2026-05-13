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
            private readonly List<Game> _games = new List<Game>();

            private int _nextGameId = 1;

            public Game AddGame(
             string gameName,
             int minPlayers,
             int maxPlayers,
             int averageGameLength)
            {
                var game = new Game(
                    _nextGameId++,
                    gameName,
                    minPlayers,
                    maxPlayers,
                    averageGameLength);

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
                return _games.Where(g => g.GameAvailability == GameAvailabilityEnum.Available);
            }
            public bool ReserveGameForMeeting(int gameId, int meetingId)
            {
            var game = GetGameById(gameId);
            if (game?.IsAvailableForBooking() != true)
                return false;

            game.MarkAsReserved();
            return true;
            }

            public void ReleaseGame(int gameId)
            {
            var game = GetGameById(gameId);
            if (game != null)
                game.MarkAsAvailable();
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

        
            public bool RemoveGame(int gameId)
            {
                var game = GetGameById(gameId);
            
                return game != null && _games.Remove(game);
            
          
            }

        }
    }
