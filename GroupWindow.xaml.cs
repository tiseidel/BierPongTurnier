using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BierPongTurnier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow()
        {
            this.InitializeComponent();
        }

        private int closeCount = 0;

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = this.closeCount++ < 5;
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(this.scroll.VerticalOffset - e.Delta / 3);
        }
    }
}