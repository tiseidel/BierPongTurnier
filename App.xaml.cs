#define SKIP_CREATION_FOR_TESTING

using BierPongTurnier.Ui;
using System.Windows;

namespace BierPongTurnier
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new ModeSelectionWindow().Show();
        }
    }
}