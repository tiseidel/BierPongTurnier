using BierPongTurnier.Model;
using BierPongTurnier.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BierPongTurnier.Ui
{
    public interface IAutoSaveCallback
    {
        void DataChanged();
    }

    public partial class ControlWindow : Window, INotifyPropertyChanged, IAutoSaveCallback
    {
        public AddDeleteTeamSetting AddDeleteTeamSetting { get; }

        public AddGameSetting AddGameSetting { get; }

        public QuickSaveSetting QuickSaveSetting { get; }

        public Tournament Tournament { get; }

        private bool _isAccidentalCloseSecureEnabled = GroupWindow.IsAccidentalCloseSecureEnabled;

        public bool IsAccidentalCloseSecureEnabled
        {
            get => this._isAccidentalCloseSecureEnabled; set
            {
                if (value != this._isAccidentalCloseSecureEnabled)
                {
                    this._isAccidentalCloseSecureEnabled = value;
                    GroupWindow.IsAccidentalCloseSecureEnabled = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ControlWindow(Tournament tournament, bool isNew)
        {
            this.InitializeComponent();

            this.Tournament = tournament;
            this.AddDeleteTeamSetting = new AddDeleteTeamSetting(tournament);
            this.AddGameSetting = new AddGameSetting(tournament);
            this.QuickSaveSetting = new QuickSaveSetting(tournament);

            if (isNew)
            {
                this.QuickSaveSetting.Export(false);
            }

            this.DataContext = this;

            foreach (Group g in this.Tournament.Groups)
            {
                //Todo use event
                g.Ranking.AutoSave = this;
            }
        }

        private int closeCount = 0;

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = this.closeCount++ < Constants.CLOSE_WINDOW_SAFETY;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DataChanged()
        {
            this.QuickSaveSetting.Export(true);
        }
    }
}