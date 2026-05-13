using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Brädhörnan_laboration
{
    public partial class MainWindow : Window
    {
        private readonly GameManager _gameManager = new();
        private readonly MemberManager _memberManager = new();
        private readonly GameMeetingManager _meetingManager = new();

        public MainWindow()
        {
            InitializeComponent();

            // Valfritt: fyller enum i combobox
            DifficultyComboBox.ItemsSource =
     System.Enum.GetValues(typeof(DifficultyLevelEnum));
        }

        // =========================
        // 🎮 SPEL
        // =========================
        private void AddGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var game = _gameManager.AddGame(
                    GameNameTextBox.Text,
                    int.Parse(MinPlayersTextBox.Text),
                    int.Parse(MaxPlayersTextBox.Text),
                    int.Parse(GameLengthTextBox.Text));

                GamesListBox.Items.Add(game);
                ClearGameInputs();

                MessageBox.Show("Spelet skapades!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GamesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedItem is Game selectedGame)
            {
                MessageBox.Show(selectedGame.ToString());
            }
        }

        // =========================
        // 👤 MEDLEMMAR
        // =========================
        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var member = _memberManager.RegisterNewMember(
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    EmailTextBox.Text,
                    PhoneTextBox.Text);

                MembersListBox.Items.Add(member);

                ClearMemberInputs();

                MessageBox.Show("Medlem skapad!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MembersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MembersListBox.SelectedItem is Member m)
            {
                MessageBox.Show(
                    "ID: " + m.MemberNumber + "\n" +
                    "Namn: " + m.FirstName + " - " + m.LastName + "\n" +
                    "Status: " + m.Status + "\n" +
                    $"Roll: {m.Role}\n" +
                    "Registrerad: " + m.RegistrationDate);
                
            }
        }

        

        // =========================
        // 🏁 SPELTRÄFFAR
        // =========================
        private void CreateMeetingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var meeting = _meetingManager.CreateGameMeeting(
                    DateTime.Now.AddDays(1),
                    LocationTextBox.Text,
                    int.Parse(MaxParticipantsTextBox.Text),
                    EventTypeEnum.Opening_evening);

                MeetingsListBox.Items.Add(meeting);

                MessageBox.Show("Spelträff skapad!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // 🧹 HJÄLP METOD
        // =========================
        private void ClearMemberInputs()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
        }
        private void ClearGameInputs()
        {
            GameNameTextBox.Clear();
            MinPlayersTextBox.Clear();
            MaxPlayersTextBox.Clear();
            GameLengthTextBox.Clear();
        }
    }
}