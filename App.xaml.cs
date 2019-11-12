#define SKIP_CREATION_FOR_TESTING

using BierPongTurnier.Model;
using BierPongTurnier.Ui;
using System;
using System.IO;
using System.Windows;

namespace BierPongTurnier
{
    public interface IStartTournamentCallback
    {
        void Start(Tournament tournament);
    }

    public partial class App : Application, IStartTournamentCallback
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.CheckAndCreateSaveFileDirectory();

            new ModeSelectionWindow()
            {
                StartTourmanentCallback = this
            }.Show();
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

        public void Start(Tournament tournament)
        {
            new ControlWindow(tournament).Show();

            foreach (Group g in tournament.Groups)
            {
                new GroupWindow(new GroupViewModel(g)).Show();
            }
        }
    }
}