using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace BierPongTurnier.Ui.Modes
{
    public interface IBeerpongConfigurationCallback
    {
        void OnBeerPongConfiguration(int groups, int teams);
    }

    public partial class ManualModeControl : UserControl, INotifyPropertyChanged, ITournamentStartMode
    {
        private string _teamCount;

        private string _groupCount;

        private string _tournamentName;

        public string TournamentName
        {
            get => this._tournamentName;
            set
            {
                this._tournamentName = value;
                this.OnPropertyChanged();
            }
        }

        public string TeamCount
        {
            get => this._teamCount;
            set
            {
                this._teamCount = value;
                this.OnPropertyChanged();
            }
        }

        public string GroupCount
        {
            get => this._groupCount;
            set
            {
                this._groupCount = value;
                this.OnPropertyChanged();
            }
        }

        public IStartTournamentCallback StartTournamentCallback { get; set; }

        public Command StartCommand { get; }

        public ManualModeControl()
        {
            this.InitializeComponent();

            this.StartCommand = new Command(this.Start, this.CanStart);

            this.DataContext = this;
        }

        private bool CanStart()
        {
            try
            {
                int gc = int.Parse(this._groupCount);
                int tc = int.Parse(this._teamCount);

                return gc > 0 && tc >= (gc * 2) && !string.IsNullOrWhiteSpace(this.TournamentName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Start()
        {
            if (!this.CanStart())
            {
                return;
            }

            int gc = int.Parse(this._groupCount);
            int tc = int.Parse(this._teamCount);

            var groups = TournamentCreator.FromCount(tc, gc);

            this?.StartTournamentCallback.Start(new Tournament(groups)
            {
                Name = this.TournamentName
            }, true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}