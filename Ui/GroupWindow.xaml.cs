using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BierPongTurnier.Ui
{
    public partial class GroupWindow : Window
    {
        public static bool IsAccidentalCloseSecureEnabled = true;

        public GroupViewModel ViewModel { get; }

        public GroupWindow(GroupViewModel viewModel)
        {
            this.InitializeComponent();

            this.ViewModel = viewModel;
            this.DataContext = viewModel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = GroupWindow.IsAccidentalCloseSecureEnabled;
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(this.scroll.VerticalOffset - e.Delta / 3);
        }

        private void DataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1) + ".";
        }
    }
}