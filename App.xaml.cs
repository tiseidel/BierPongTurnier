#define SKIP_CREATION_FOR_TESTING

using BierPongTurnier.Settings;
using BierPongTurnier.Ui;
using System;
using System.IO;
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

            /*
            this.CheckAndCreateSaveFileDirectory();

            new ModeSelectionWindow().Show();
            */
            new ExportToKnockoutStage().Test();
        }

        private void CheckAndCreateSaveFileDirectory()
        {
            try
            {
                if (!Directory.Exists(Constants.DIR_SAVEFILES))
                {
                    Directory.CreateDirectory(Constants.DIR_SAVEFILES);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}