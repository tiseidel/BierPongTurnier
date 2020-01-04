using BierPongTurnier.Common;
using BierPongTurnier.Model;
using BierPongTurnier.Persist;
using BierPongTurnier.Persist.Dto;
using BierPongTurnier.Ui;
using BierPongTurnier.Ui.Modes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BierPongTurnier
{
    public partial class StartupWindow : Window, IStartTournamentCallback
    {
        public ICommand ShowModePlayerCommand { get; set; }

        public ICommand ShowModeTeamCommand { get; set; }

        public ICommand ShowModeManualCommand { get; set; }

        public ICommand LoadTournamentCommand { get; set; }

        public StartupWindow()
        {
            this.InitializeComponent();
            this.ShowModePlayerCommand = new Command(() => this.ShowMode("Zufällige Einteilung der Spieler in Teams und Gruppen", new RandomPlayerModeControl() { StartTournamentCallback = this }));
            this.ShowModeTeamCommand = new Command(() => this.ShowMode("Zufällige Einteilung der Teams in Gruppen", new RandomTeamModeControl() { StartTournamentCallback = this }));
            this.ShowModeManualCommand = new Command(() => this.ShowMode("Zufällige Gruppenvorlagen", new ManualModeControl() { StartTournamentCallback = this }));
            this.LoadTournamentCommand = new Command(this.ShowExisitingTournamentSelection);

            this.DataContext = this;
        }

        private void ShowMode(string title, UserControl control)
        {
            new Window()
            {
                Title = title,
                Width = 800,
                Height = 600,
                Content = control
            }.Show();
        }

        private void ShowExisitingTournamentSelection()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var tournament = new JsonService().Import(openFileDialog.FileName);
                    this.Start(tournament, false);
                }
                catch (Exception)
                {
                    MessageBox.Show("Datei konnte nicht geladen werden (Bitte eine .beer-Datei auswählen).", "Fehler beim Laden");
                }
            }
        }

        public void Start(Tournament tournament, bool isNew)
        {
            new ControlWindow(tournament, isNew).Show();

            foreach (Group g in tournament.Groups)
            {
                new GroupWindow(new GroupViewModel(g)).Show();
            }
        }
    }
}