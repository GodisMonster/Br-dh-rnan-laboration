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

           
            DifficultyComboBox.ItemsSource = System.Enum.GetValues(typeof(DifficultyLevelEnum));
            RollComboBox.ItemsSource = System.Enum.GetValues(typeof(MemberRoleEnum));

        }

      
      
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

      
        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var member = _memberManager.RegisterNewMember(
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    EmailTextBox.Text,
                    PhoneTextBox.Text,
                    RollComboBox.Text);

                MembersListBox.Items.Add(member);

                ClearMemberInputs();

                MessageBox.Show("Medlem skapad!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
  

     
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