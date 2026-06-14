using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Brädhörnan_laboration.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private readonly IGameService _gameService;
        public event Action? DataChanged;

        public ObservableCollection<Game> Games { get; } = new();
        public IEnumerable<DifficultyLevelEnum> DifficultyLevels => System.Enum.GetValues<DifficultyLevelEnum>();
        public IEnumerable<GamegenreEnum> Genres => System.Enum.GetValues<GamegenreEnum>();

        [ObservableProperty] private Game? _selectedGame;
        [ObservableProperty] private string _gameName = "";
        [ObservableProperty] private string _minPlayers = "";
        [ObservableProperty] private string _maxPlayers = "";
        [ObservableProperty] private string _gameLength = "";
        [ObservableProperty] private DifficultyLevelEnum _selectedDifficulty;
        [ObservableProperty] private GamegenreEnum _selectedGenre;

        public GameViewModel(IGameService gameService)
        {
            _gameService = gameService;
            LoadGames();
        }

        public void LoadGames()
        {
            Games.Clear();
            foreach (var game in _gameService.GetAllGames())
                Games.Add(game);
        }

        [RelayCommand]
        private void AddGame()
        {
            try
            {
                if (!int.TryParse(MinPlayers, out int min)) { MessageBox.Show("Ogiltigt minsta antal spelare."); return; }
                if (!int.TryParse(MaxPlayers, out int max)) { MessageBox.Show("Ogiltigt max antal spelare."); return; }
                if (!int.TryParse(GameLength, out int length)) { MessageBox.Show("Ogiltig speltid."); return; }

                var id = _gameService.GetNextGameId();
                var game = new Game(id, GameName, min, max, length, SelectedDifficulty, SelectedGenre);
                _gameService.AddGame(game);
                LoadGames();
                ClearForm();
                DataChanged?.Invoke();
                MessageBox.Show("Spel tillagt!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void UpdateGame()
        {
            if (SelectedGame == null) { MessageBox.Show("Välj ett spel."); return; }
            try
            {
                if (!int.TryParse(MinPlayers, out int min)) { MessageBox.Show("Ogiltigt minsta antal spelare."); return; }
                if (!int.TryParse(MaxPlayers, out int max)) { MessageBox.Show("Ogiltigt max antal spelare."); return; }
                if (!int.TryParse(GameLength, out int length)) { MessageBox.Show("Ogiltig speltid."); return; }

                SelectedGame.UpdateGame(GameName, min, max, length, SelectedDifficulty, SelectedGenre);
                _gameService.UpdateGame(SelectedGame);
                LoadGames();
                ClearForm();
                SelectedGame = null;
                DataChanged?.Invoke();
                MessageBox.Show("Spelet uppdaterat.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void RemoveGame()
        {
            if (SelectedGame == null) { MessageBox.Show("Välj ett spel."); return; }
            var result = MessageBox.Show($"Ta bort {SelectedGame.GameName}?", "Bekräfta", MessageBoxButton.OKCancel);
            if (result != MessageBoxResult.OK) return;
            try
            {
                _gameService.RemoveGame(SelectedGame);
                LoadGames();
                SelectedGame = null;
                DataChanged?.Invoke();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void GroupByGenre()
        {
            Games.Clear();
            foreach (var group in _gameService.GetAllGames().GroupBy(g => g.Gamegenre))
                foreach (var game in group)
                    Games.Add(game);
        }

        private void ClearForm()
        {
            GameName = "";
            MinPlayers = "";
            MaxPlayers = "";
            GameLength = "";
        }
    }
}