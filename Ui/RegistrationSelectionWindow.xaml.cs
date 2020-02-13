using System.Collections.Generic;
using System.Windows;

namespace BierPongTurnier.Ui
{
    public partial class RegistrationSelectionWindow : Window
    {
        internal interface INavigationCallback
        {
            void GotoTeamModeWindow(List<string> teams);

            void ShowErrorMessage(string title, string description);
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