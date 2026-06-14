using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Brädhörnan_laboration.Models;
using Brädhörnan_laboration.Enum;
using Brädhörnan_laboration.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Brädhörnan_laboration.ViewModels
{
    public partial class MemberViewModel : ObservableObject
    {
        private readonly IMemberService _memberService;
        public event Action? DataChanged;

        public ObservableCollection<Member> Members { get; } = new();
        public IEnumerable<MemberRoleEnum> Roles => System.Enum.GetValues<MemberRoleEnum>();
        public IEnumerable<MemberStatusEnum> Statuses => System.Enum.GetValues<MemberStatusEnum>();

        [ObservableProperty] private Member? _selectedMember;
        [ObservableProperty] private string _firstName = "";
        [ObservableProperty] private string _lastName = "";
        [ObservableProperty] private string _email = "";
        [ObservableProperty] private string _phone = "";
        [ObservableProperty] private MemberRoleEnum _selectedRole;
        [ObservableProperty] private MemberStatusEnum _selectedStatus;

        public MemberViewModel(IMemberService memberService)
        {
            _memberService = memberService;
            _ = LoadMembersAsync();
        }

        public async Task LoadMembersAsync()
        {
            Members.Clear();
            foreach (var member in await _memberService.GetAllMembersAsync())
                Members.Add(member);
        }

        public void LoadMembers()
        {
            Members.Clear();
            foreach (var member in _memberService.GetAllMembers())
                Members.Add(member);
        }

        [RelayCommand]
        private void AddMember()
        {
            try
            {
                var id = _memberService.GetNextMemberNumber();
                var member = new Member(id, FirstName, LastName, Email, SelectedStatus, SelectedRole, Phone);
                _memberService.AddMember(member);
                LoadMembers();
                ClearForm();
                DataChanged?.Invoke();
                MessageBox.Show("Medlem skapad!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void UpdateMember()
        {
            if (SelectedMember == null) { MessageBox.Show("Välj en medlem."); return; }
            try
            {
                SelectedMember.UpdateName(FirstName, LastName);
                SelectedMember.UpdateEmail(Email);
                SelectedMember.UpdatePhone(Phone);
                SelectedMember.UpdateRole(SelectedRole);
                SelectedMember.UpdateStatus(SelectedStatus);
                _memberService.UpdateMember(SelectedMember);
                LoadMembers();
                SelectedMember = null;
                DataChanged?.Invoke();
                MessageBox.Show("Medlem uppdaterad.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void RemoveMember()
        {
            if (SelectedMember == null) { MessageBox.Show("Välj en medlem."); return; }
            var result = MessageBox.Show($"Ta bort {SelectedMember.FullName}?", "Bekräfta", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result != MessageBoxResult.OK) return;
            try
            {
                _memberService.RemoveMember(SelectedMember);
                LoadMembers();
                SelectedMember = null;
                DataChanged?.Invoke();
                MessageBox.Show("Medlem borttagen.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        [RelayCommand]
        private void SortByName()
        {
            Members.Clear();
            foreach (var m in _memberService.GetMembersSortedByName())
                Members.Add(m);
        }

        [RelayCommand]
        private void FilterActive()
        {
            Members.Clear();
            foreach (var m in _memberService.GetActiveMembers())
                Members.Add(m);
        }

        private void ClearForm()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
        }
    }
}