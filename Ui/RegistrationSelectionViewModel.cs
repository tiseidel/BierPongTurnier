using BierPongTurnier.Common;
using BierPongTurnier.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BierPongTurnier.Ui
{
    public class RegistrationSelectionViewModel : BaseNotifyPropertyChanged
    {
        internal RegistrationSelectionWindow.INavigationCallback NavigationCallback { get; set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get => this._isLoading;
            set
            {
                if (this._isLoading != value)
                {
                    this._isLoading = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private List<TournamentRegistration> _registrations;

        public List<TournamentRegistration> Registrations
        {
            get => this._registrations;
            set
            {
                if (this._registrations != value)
                {
                    this._registrations = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private TournamentRegistration _selectedRegistration;

        public TournamentRegistration SelectedRegistration
        {
            get => this._selectedRegistration;
            set
            {
                if (this._selectedRegistration != value)
                {
                    this._selectedRegistration = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Command RegistrationSelectedCommand { get; set; }

        private readonly ITournamentRegistrationService _tournamentRegistrationService = new TournamentRegistrationService();

        public RegistrationSelectionViewModel()
        {
            this.RegistrationSelectedCommand = new Command(() => this.StartMode(this.SelectedRegistration), () => this.SelectedRegistration != null && this.Registrations != null && this.Registrations.Count > 0);
        }

        public async Task LoadRegistrations()
        {
            this.IsLoading = true;
            var ids = await this._tournamentRegistrationService.GetTournamentRegistrationIds();
            if (ids.Count > 0)
            {
                var regs = new List<TournamentRegistration>();
                foreach (var id in ids)
                {
                    var r = await this._tournamentRegistrationService.GetTournamentRegistration(id);
                    regs.Add(r);
                }
                this.Registrations = regs;
            }
            this.IsLoading = false;
        }

        private void StartMode(TournamentRegistration tournamentRegistration)
        {
            if (tournamentRegistration == null) return;

            var teams = new List<string>();
            foreach (var r in tournamentRegistration.Teams)
            {
                teams.Add(r.Player1Name + " + " + r.Player2Name);
            }

            this.NavigationCallback?.GotoTeamModeWindow(teams);
        }
    }
}