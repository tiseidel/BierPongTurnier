using BierPongTurnier.Ui.Modes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BierPongTurnier.Ui
{
    public class Mode
    {
        public string Title { get; set; }

        public ICommand Command { get; set; }
    }

    public partial class ModeSelectionWindow : Window, INotifyPropertyChanged
    {
        private UserControl _ui;

        public ObservableCollection<Mode> Modes { get; set; }

        public UserControl UI
        {
            get => this._ui;
            set
            {
                this._ui = value;
                this.OnPropertyChanged();
            }
        }

        public ModeSelectionWindow()
        {
            this.InitializeComponent();

            this.Modes = new ObservableCollection<Mode>()
            {
                new Mode(){ Title = "Zufall (Spieler)", Command = new Command(this.SelectRandomPlayer)},
                new Mode(){ Title = "Zufall (Teams)", Command = new Command(this.SelectRandomTeam)},
                new Mode(){ Title = "Manuell", Command = new Command(this.SelectManualMode)},
                new Mode(){ Title = "Importieren", Command = new Command(this.SelectImport)}
            };

            this.DataContext = this;
        }

        public void SelectRandomPlayer()
        {
            this.UI = new RandomPlayerModeControl();
        }

        public void SelectRandomTeam()
        {
            this.UI = new RandomTeamModeControl();
        }

        public void SelectManualMode()
        {
            this.UI = new ManualModeControl();
        }

        public void SelectImport()
        {
            this.UI = new ImportModeControl();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}