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
        private Member? _selectedMember = null;
        private Game? _selectedGame = null;

        public MainWindow()
        {
            InitializeComponent();

           
      
            RollComboBox.ItemsSource = System.Enum.GetValues(typeof(MemberRoleEnum));
            StatusComboBox.ItemsSource = System.Enum.GetValues(typeof(MemberStatusEnum));
            DifficultyComboBox.ItemsSource = System.Enum.GetValues(typeof(DifficultyLevelEnum));
            
        }
        
        private void AddGameButton_Click(object sender, RoutedEventArgs e) // Metoder i Lägg till spel knappen
        {
            try
            {
                var game = _gameManager.AddGame(
                    GameNameTextBox.Text,
                    int.Parse(MinPlayersTextBox.Text),
                    int.Parse(MaxPlayersTextBox.Text),
                    int.Parse(GameLengthTextBox.Text),
                    DifficultyComboBox.Text);

                GamesListBox.Items.Add(game);

                RefreshMemberList();
             
                ClearGameInputs();

                MessageBox.Show("Spelet skapades!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedGame == null)
                return;

            try
            {
                _selectedGame.UpdateGame(
                 GameNameTextBox.Text,
                 int.Parse(MinPlayersTextBox.Text),
                 int.Parse(MaxPlayersTextBox.Text),
                 int.Parse(GameLengthTextBox.Text),
                 (DifficultyLevelEnum)DifficultyComboBox.SelectedItem);
    

                RefreshGameList();
                ClearGameInputs();

                AddGameButton.Visibility = Visibility.Visible;
                UpdateGameButton.Visibility = Visibility.Visible;

                MessageBox.Show("Spelet uppdaterat.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveGameButton_Click(object sender, RoutedEventArgs e)
        {
            if ( _selectedGame == null) return;

            try
            {
                bool removed = _gameManager.RemoveGame(_selectedGame.GameId);

                if( removed)
                {
                    RefreshGameList();
                    ClearGameInputs();

                    _selectedGame = null;

                    MessageBox.Show("Spelet har tagits bort.");
                }
                else
                {
                    MessageBox.Show("Spelet hittades inte.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e) // Metoder i Lägg till medlem knappen
        {
            try
            {
                var member = _memberManager.RegisterNewMember(
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    EmailTextBox.Text,
                    PhoneTextBox.Text,
                    StatusComboBox.Text,
                    RollComboBox.Text);

                MembersListBox.Items.Add(member);
                RefreshMemberList();
                ClearMemberInputs();

                MessageBox.Show("Medlem skapad!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMember == null) return;

            try
            {
                _selectedMember.UpdateName(FirstNameTextBox.Text, LastNameTextBox.Text);
                _selectedMember.UpdateEmail(EmailTextBox.Text);
                _selectedMember.UpdatePhone(PhoneTextBox.Text);
                _selectedMember.UpdateRole((MemberRoleEnum)RollComboBox.SelectedItem);
                _selectedMember.UpdateStatus((MemberStatusEnum)StatusComboBox.SelectedItem);

                RefreshMemberList();
                ClearMemberInputs();
                ResetMemberForm();

                MessageBox.Show("Medlem uppdaterad.");
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }

        }
        private void RemoveMemberButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMember == null) return;
            {
                try
                {
                    bool removed = _memberManager.RemoveMember(_selectedMember.MemberNumber);

                    if (removed)
                    {

                        RefreshMemberList();
                        ClearMemberInputs();
                        ResetMemberForm();

                        MessageBox.Show("Medlem bortagen");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kunde inte ta bort medlem:{ex.Message}");
                }
            }
        }
        


        private void CreateMeetingButton_Click(object sender, RoutedEventArgs e) // Metoder i Skapa möte knappen
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

       // Hjälpfunktioner för snyggare UI
        private void ClearMemberInputs()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
            StatusComboBox.SelectedItem = null;
            RollComboBox.SelectedItem = null;

        }
        private void ClearGameInputs()
        {
            GameNameTextBox.Clear();
            MinPlayersTextBox.Clear();
            MaxPlayersTextBox.Clear();
            GameLengthTextBox.Clear();
            DifficultyComboBox.SelectedItem = null;
        }
        private void RefreshMemberList()
        {
            MembersListBox.Items.Clear();
            foreach(var member in _memberManager.GetAllMembers())
            {
                MembersListBox.Items.Add(member);
            }
        }
        private void RefreshGameList()
        {
            GamesListBox.Items.Clear();

            foreach (var game in _gameManager.GetAllGames())
            {
                GamesListBox.Items.Add(game);
            }
        }
        private void ResetMemberForm()
        {
            _selectedMember = null;
            AddMemberButton.Visibility = Visibility.Visible;
            UpdateMemberButton.Visibility = Visibility.Collapsed;
            MembersListBox.SelectedItem = null;
        }
     


        // Listboxar

        private void GamesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedItem is Game selectedGame)
            {
                _selectedGame = selectedGame;

                GameNameTextBox.Text = selectedGame.GameName;
                MinPlayersTextBox.Text = selectedGame.MinimumNumberOfPlayer.ToString();
                MaxPlayersTextBox.Text = selectedGame.MaximumNumberOfPlayer.ToString();
                GameLengthTextBox.Text = selectedGame.AverageGameLength.ToString();


                AddGameButton.Visibility = Visibility;
                UpdateGameButton.Visibility = Visibility;

                
                MessageBox.Show(selectedGame.ToString());
            }
        }
        private void MemberListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(MembersListBox.SelectedItem is Member selectedMember)
            {
                _selectedMember = selectedMember;

                FirstNameTextBox.Text = selectedMember.FirstName;
                LastNameTextBox.Text = selectedMember.LastName;
                EmailTextBox.Text = selectedMember.Email;
                PhoneTextBox.Text = selectedMember.Phone;
                StatusComboBox.SelectedItem = selectedMember.Status;
                RollComboBox.SelectedItem = selectedMember.Role;

               
                AddMemberButton.Visibility = Visibility.Visible;
                UpdateMemberButton.Visibility = Visibility.Visible;
            }
        }
    }



}