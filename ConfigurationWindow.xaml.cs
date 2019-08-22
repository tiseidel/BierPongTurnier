using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BierPongTurnier
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window, INotifyPropertyChanged
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

        public App App { get; private set; }

        public ConfigurationWindow(App app)
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.App = app;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.GroupCount > 0 && this.TeamCount >= (this.GroupCount * 2))
            {
                this.App.OnBeerPongConfiguration(this.GroupCount, this.TeamCount);
                this.App = null;
                this.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}