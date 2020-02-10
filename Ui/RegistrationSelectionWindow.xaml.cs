using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BierPongTurnier.Ui
{
    public partial class RegistrationSelectionWindow : Window
    {
        internal interface INavigationCallback
        {
            void GotoTeamModeWindow(List<string> teams);
        }

        public RegistrationSelectionViewModel ViewModel { get; }

        public RegistrationSelectionWindow()
        {
            this.InitializeComponent();
            this.ViewModel = new RegistrationSelectionViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}