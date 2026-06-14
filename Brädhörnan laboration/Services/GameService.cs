using Brädhörnan_laboration.Data;
using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;

namespace Brädhörnan_laboration.Services
{
    public class GameService : IGameService
    {
        public IEnumerable<Game> GetAllGames()
        {
            using var context = new AppDbContext();
            return context.Games.ToList();
        }

        public IEnumerable<Game> GetAvailableGames()
        {
            using var context = new AppDbContext();
            return context.Games
                .Where(g => g.GameAvailability == GameAvailabilityEnum.Available)
                .ToList();
        }

        public Game? GetGameById(int gameId)
        {
            using var context = new AppDbContext();
            return context.Games
                .FirstOrDefault(g => g.GameId == gameId);
        }

        public void AddGame(Game game)
        {
            using var context = new AppDbContext();
            context.Games.Add(game);
            context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            using var context = new AppDbContext();
            context.Games.Update(game);
            context.SaveChanges();
        }

        public void RemoveGame(Game game)
        {
            using var context = new AppDbContext();
            context.Games.Remove(game);
            context.SaveChanges();
        }

        public int GetNextGameId()
        {
            using var context = new AppDbContext();
            return context.Games.Any()
                ? context.Games.Max(g => g.GameId) + 1
                : 1;
        }
    }
}