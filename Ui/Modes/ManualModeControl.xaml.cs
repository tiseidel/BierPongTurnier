using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BierPongTurnier.Ui.Modes
{
    public interface IBeerpongConfigurationCallback
    {
        void OnBeerPongConfiguration(int groups, int teams);
    }

    public partial class ManualModeControl : UserControl, INotifyPropertyChanged
    {
        private int _teamCount;

        private int _groupCount;

        public int TeamCount
        {
            get => this._teamCount;
            set
            {
                this._teamCount = value;
                this.OnPropertyChanged();
            }
        }

        public int GroupCount
        {
            get => this._groupCount;
            set
            {
                this._groupCount = value;
                this.OnPropertyChanged();
            }
        }

        public IBeerpongConfigurationCallback ConfigurationCallback { get; private set; }

        public ManualModeControl(IBeerpongConfigurationCallback configurationCallback)
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.ConfigurationCallback = configurationCallback;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.GroupCount > 0 && this.TeamCount >= (this.GroupCount * 2))
            {
                this.ConfigurationCallback.OnBeerPongConfiguration(this.GroupCount, this.TeamCount);
                this.ConfigurationCallback = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}