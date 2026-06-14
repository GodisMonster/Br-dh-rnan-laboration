using Brädhörnan_laboration.Models;

namespace Brädhörnan_laboration.Services
{
    public interface IGameService
    {
        IEnumerable<Game> GetAllGames();
        IEnumerable<Game> GetAvailableGames();
        Game? GetGameById(int gameId);
        void AddGame(Game game);
        void UpdateGame(Game game);
        void RemoveGame(Game game);
        int GetNextGameId();
    }
}