using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        private GameMeeting _selectedMeeting = null;

        public MainWindow()
        {
            InitializeComponent();

            RollComboBox.ItemsSource = System.Enum.GetValues(typeof(MemberRoleEnum));
            StatusComboBox.ItemsSource = System.Enum.GetValues(typeof(MemberStatusEnum));
            DifficultyComboBox.ItemsSource = System.Enum.GetValues(typeof(DifficultyLevelEnum));
            EventTypeComboBox.ItemsSource = System.Enum.GetValues(typeof(EventTypeEnum));
            GenreComboBox.ItemsSource= System.Enum.GetValues(typeof(GamegenreEnum));
            RefreshAvailableGames();
            


            StatusComboBox.SelectedItem = MemberStatusEnum.Active;
            RollComboBox.SelectedItem = MemberRoleEnum.Member;
            DifficultyComboBox.SelectedItem = DifficultyLevelEnum.Easy;
            GenreComboBox.SelectedItem = GamegenreEnum.Unknown; 

            RefreshAvailableMembers();



        }
        private void AddGameButton_Click(object sender, RoutedEventArgs e) // Metoder i Lägg till spel knappen
        {

            try
            {
                if (!int.TryParse(MinPlayersTextBox.Text, out int minPlayers))
                {
                    MessageBox.Show("Minsta antal spelare måste vara minst 1.");
                    return;
                }
                if (!int.TryParse(MaxPlayersTextBox.Text, out int maxPlayers))
                {
                    MessageBox.Show("Max antal spelare måste vara högre än minsta antal spelare.");
                    return;
                }
                if (!int.TryParse(GameLengthTextBox.Text, out int gameLength))
                {
                    MessageBox.Show("Speltid måste vara ett nummer.");
                    return;
                }

                _gameManager.AddGame(
                   GameNameTextBox.Text,
                           minPlayers,
                           maxPlayers,
                           gameLength,
                            (DifficultyLevelEnum)DifficultyComboBox.SelectedItem,
                            (GamegenreEnum)GenreComboBox.SelectedItem);

                RefreshGameList();
                RefreshAvailableGames();
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
                 (DifficultyLevelEnum)DifficultyComboBox.SelectedItem,
                 (GamegenreEnum)GenreComboBox.SelectedItem);


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
            if (_selectedGame == null) return;

            try
            {
                bool removed = _gameManager.RemoveGame(_selectedGame.GameId); // Metod från annan klass

                if (removed)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e) // Metoder i Lägg till medlem knappen
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
                {
                    MessageBox.Show("Fällt måste fyllas.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                {
                    MessageBox.Show("Fällt måste fyllas.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    MessageBox.Show("E-post måste anges.");
                    return;
                }

                string phone = PhoneTextBox.Text ?? "";
                // Metod från MemberManager
                var member = _memberManager.RegisterNewMember(
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    EmailTextBox.Text,
                    phone,
                    (MemberStatusEnum)StatusComboBox.SelectedItem,
                    (MemberRoleEnum)RollComboBox.SelectedItem);


                RefreshMemberList();
                RefreshAvailableMembers();
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

                if (MeetingDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Välj datum.");
                    return;
                }
                if (!TimeSpan.TryParse(MeetingTimeTextBox.Text, out TimeSpan time))
                {
                    MessageBox.Show("Ange giltid tid. Exempel 17:45.");
                    return;
                }
                if (EventTypeComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Välj eventtyp.");
                    return;
                }
                DateTime dateAndTime = MeetingDatePicker.SelectedDate.Value.Date + time;

                var eventType = (EventTypeEnum)EventTypeComboBox.SelectedItem;


                var meeting = _meetingManager.CreateGameMeeting(
                    dateAndTime,
                    LocationTextBox.Text,
                    int.Parse(MaxParticipantsTextBox.Text),
                    eventType);

                if(ResponsibleComboBox.SelectedItem is Member selectedResponsible)
                {
                    meeting.SetResponsible(selectedResponsible);
                }

                RefreshMeetingList();
                ClearGameMeetingInputs();
                RefreshPlannedGamesList();

                MessageBox.Show("Spelträff skapad!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMeeting == null)
            {
                MessageBox.Show("Välj en spelträff.");
                return;
            }
            if (AvailableMembersComboBox.SelectedItem is not Member selectedMember)
            {
                MessageBox.Show("Välj en medlem att lägga till.");
                return;
            }
            try
            {
                var (success, message) = _meetingManager.RegisterParticipant(
                    _selectedMeeting.GameMeetingId,
                    selectedMember);

                if (success)
                {
                    RefreshMeetingParticipantsList();
                    RefreshMeetingList();
                }

                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}");
            }
        }
        private void UnRegisterParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMeeting == null)
            {
                MessageBox.Show("Välj en spelträff först.");
                return;
            }

            if (MeetingParticipantsListBox.SelectedItem is not Member selectedParticipant)
            {
                MessageBox.Show("Välj en deltagare i deltagarlistan att ta bort.");
                return;
            }

            try
            {
                var (success, message) = _meetingManager.UnregisterParticipant(
                _selectedMeeting.GameMeetingId,
                 selectedParticipant);

                if (success)
                {
                    RefreshMeetingParticipantsList();
                    RefreshMeetingList();
                }

                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett oväntat fel inträffade: {ex.Message}");
            }
        }
        private void RefreshMemberList()
        {
            MembersListBox.Items.Clear();
            foreach (var member in _memberManager.GetAllMembers()) // Metod som hämtas från klass
            {
                MembersListBox.Items.Add(member);
            }
        }
        private void RefreshGameList()
        {
            GamesListBox.Items.Clear();

            foreach (var game in _gameManager.GetAllGames()) // Metod som hämtas från klass
            {
                GamesListBox.Items.Add(game);
            }
        }
        private void RefreshAvailableGames()
{
    
    AvailableGamesComboBox.Items.Clear();

    var allGames = _gameManager.GetAllGames();
    if (allGames == null) return;

  
    if (_selectedMeeting == null)
    {
        foreach (var game in allGames)
        {
            AvailableGamesComboBox.Items.Add(game);
        }
        return; 
    }

   
    if (_selectedMeeting.PlannedGames == null)
    {
        foreach (var game in allGames)
        {
            AvailableGamesComboBox.Items.Add(game);
        }
        return; 
    }

   
    var availableGames = allGames.Where(game => 
        !_selectedMeeting.PlannedGames.Any(pg => pg.GameId == game.GameId));

    foreach (var game in availableGames)
    {
        AvailableGamesComboBox.Items.Add(game);
    }
}
        private void ResetMemberForm()
        {
            _selectedMember = null;
            AddMemberButton.Visibility = Visibility.Visible;
            UpdateMemberButton.Visibility = Visibility.Collapsed;
            MembersListBox.SelectedItem = null;
        }

        private void RefreshMeetingList()
        {
            MeetingsListBox.Items.Clear();

            foreach (var meeting in _meetingManager.GetAllMeetings())
            {
                MeetingsListBox.Items.Add(meeting);
            }
        }
        private void RefreshMeetingParticipantsList()
        {
            MeetingParticipantsListBox.Items.Clear();

            if (_selectedMeeting == null)
                return;

            foreach (var participant in _selectedMeeting.Participants)
            {
                MeetingParticipantsListBox.Items.Add(participant);
            }
        }

        private void RefreshAvailableMembers()
        {
            AvailableMembersComboBox.Items.Clear();
            ResponsibleComboBox.Items.Clear();

            foreach (var member in _memberManager.GetAllMembers())
            {
                AvailableMembersComboBox.Items.Add(member);
                ResponsibleComboBox.Items.Add(member);
            }
        }
        private void RefreshPlannedGamesList()
        {
            PlannedGamesListBox.Items.Clear();

            if (_selectedMeeting == null)
                return;

            foreach (var game in _selectedMeeting.PlannedGames)
            {
                PlannedGamesListBox.Items.Add(game);
            }
        }
        private void BookGame_Click(object sender, RoutedEventArgs e)
        {

         if (_selectedMeeting == null)
            {
                MessageBox.Show("Välj en spelträff.");
                return;
            }
         if(AvailableGamesComboBox.SelectedItem is not Game selectedGame)
            {
                MessageBox.Show("Välj ett spel att boka.");
                return;
            }
         try
            {
                var (success, message) = _meetingManager.AddGameToMeeting(
           _selectedMeeting.GameMeetingId,
           selectedGame);

                if (success)
                {
                    RefreshPlannedGamesList();
                    RefreshAvailableGames(); 
                    MessageBox.Show($"Bokningen lyckades: {message}");
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid bokning: {ex.Message}");
            }
        }
            

        
        private void UnregisterGame_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMeeting == null)
            {
                MessageBox.Show("Välj en spelträff först.");
                return;
            }

            if (PlannedGamesListBox.SelectedItem is not Game selectedGame)
            {
                MessageBox.Show("Välj ett spel att avboka.");
                return;
            }

            try
            {
                var (success, message) = _meetingManager.RemoveGameFromMeeting(
                    _selectedMeeting.GameMeetingId,
                    selectedGame);

                if (success)
                {
                    RefreshAvailableGames();      
                    RefreshPlannedGamesList();   
                    MessageBox.Show($"Avbokning lyckades: {message}");
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid avbokning: {ex.Message}");
            }
        }

        private void GamesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GamesListBox.SelectedItem is Game selectedGame)
            {
                _selectedGame = selectedGame;

                GameNameTextBox.Text = selectedGame.GameName;
                MinPlayersTextBox.Text = selectedGame.MinimumNumberOfPlayer.ToString();
                MaxPlayersTextBox.Text = selectedGame.MaximumNumberOfPlayer.ToString();
                GameLengthTextBox.Text = selectedGame.AverageGameLength.ToString();
                GenreComboBox.SelectedItem = selectedGame.Gamegenre;


                AddGameButton.Visibility = Visibility.Visible;
                UpdateGameButton.Visibility = Visibility.Visible;


            }
        }
        private void MemberListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MembersListBox.SelectedItem is Member selectedMember)
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
        private void MeetingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MeetingsListBox.SelectedItem is GameMeeting selectedMeeting)
            {
                _selectedMeeting = selectedMeeting;

                RefreshMeetingParticipantsList();
                RefreshPlannedGamesList();
                RefreshAvailableGames();
            }
        }
        // Hjälpfunktioner för snyggare UI
        private void ClearMemberInputs()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
            StatusComboBox.SelectedItem = MemberStatusEnum.Active;
            RollComboBox.SelectedItem = MemberRoleEnum.Member;
        }
        private void ClearGameInputs()
        {
            GameNameTextBox.Clear();
            MinPlayersTextBox.Clear();
            MaxPlayersTextBox.Clear();
            GameLengthTextBox.Clear();
            DifficultyComboBox.SelectedItem = DifficultyLevelEnum.Easy;
            GenreComboBox.SelectedItem = GamegenreEnum.Unknown;
        }
        private void ClearGameMeetingInputs()
        {
            LocationTextBox.Clear();
            MaxParticipantsTextBox.Clear();
            ResponsibleComboBox.SelectedItem = null;
            EventTypeComboBox.SelectedItem = null;
            MeetingDatePicker.SelectedDate = null;
            MeetingTimeTextBox.Clear();
            

        }
        // LINQ-FUNKTIONER
        private void GroupByGenre_Click(object sender, RoutedEventArgs e)
        {
            GamesListBox.Items.Clear();

            var groupedGames = _gameManager.GetAllGames().GroupBy(g => g.Gamegenre);

            foreach(var group in groupedGames)
            {
                GamesListBox.Items.Add($"Genre: {group.Key}");

                foreach(var game in group)
                {
                    GamesListBox.Items.Add(game);
                }
            }
        }
        private void FilterStatusMemberButton_Click(object sender, RoutedEventArgs e) 
        {
            MembersListBox.Items.Clear();

            var activeMembers = _memberManager.GetActiveMembers();
            foreach( var member in activeMembers)
            {
                MembersListBox.Items.Add(member);
            }
        }
        private void SortMemberByName_Click(object sender, RoutedEventArgs e)
        {
            MembersListBox.Items.Clear();

            var sorteringMedlem = _memberManager.GetMembersSortedByName();
            foreach(var member in sorteringMedlem)
            {
                MembersListBox.Items.Add(member);
            }
            MessageBox.Show("Sorterade först genom efternamn.");
        }
    }
}