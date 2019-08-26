using BierPongTurnier.Model;
using BierPongTurnier.Ui.Settings;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BierPongTurnier.Ui
{
    /// <summary>
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    ///
    public interface IAutoSaveCallback
    {
        void DataChanged();
    }

    public partial class ControlWindow : Window, INotifyPropertyChanged, IAutoSaveCallback
    {
        public AddDeleteTeamSetting AddDeleteTeamSetting { get; }

        public AddGameSetting AddGameSetting { get; }

        public ExportCSVSetting ExportCSVSetting { get; }

        public Tournament Tournament { get; }

        public ControlWindow(Tournament tournament)
        {
            this.InitializeComponent();

            this.Tournament = tournament;
            this.AddDeleteTeamSetting = new AddDeleteTeamSetting(tournament);
            this.AddGameSetting = new AddGameSetting(tournament);
            this.ExportCSVSetting = new ExportCSVSetting(tournament);

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
            this.ExportCSVSetting.ToCSV(true);
        }
    }
}