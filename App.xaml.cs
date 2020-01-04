#define SKIP_CREATION_FOR_TESTING

using BierPongTurnier.Model;
using System;
using System.IO;
using System.Windows;

namespace BierPongTurnier
{
    public interface IStartTournamentCallback
    {
        void Start(Tournament tournament, bool isNew);
    }

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.CheckAndCreateSaveFileDirectory();

            new StartupWindow().Show();
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