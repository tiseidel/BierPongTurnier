using BierPongTurnier.Model;
using BierPongTurnier.Ui.Modes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BierPongTurnier.Ui
{
    public class Mode
    {
        public string Title { get; set; }

        public ICommand Command { get; set; }
    }

    public partial class ModeSelectionWindow : Window, IBeerpongConfigurationCallback, INotifyPropertyChanged
    {
        private UserControl _ui;

        public ObservableCollection<Mode> Modes { get; set; }

        public UserControl UI
        {
            get => this._ui;
            set
            {
                this._ui = value;
                this.OnPropertyChanged();
            }
        }

        public ModeSelectionWindow()
        {
            this.InitializeComponent();

            this.Modes = new ObservableCollection<Mode>()
            {
                new Mode(){ Title = "Zufall (Spieler)", Command = new Command(this.SelectRandomPlayer)},
                new Mode(){ Title = "Zufall (Teams)", Command = new Command(this.SelectRandomTeam)},
                new Mode(){ Title = "Manuell", Command = new Command(this.SelectManualMode)},
                new Mode(){ Title = "Importieren", Command = new Command(this.SelectManualMode)}
            };

            this.DataContext = this;
        }

        public void SelectRandomPlayer()
        {
            this.UI = new RandomPlayerModeControl();
        }

        public void SelectRandomTeam()
        {
            this.UI = new RandomTeamModeControl();
        }

        public void SelectManualMode()
        {
            this.UI = new ManualModeControl(this);
        }

        public void OnBeerPongConfiguration(int groupCount, int teamCount)
        {
            int left = 0; int right = 1;
            var groups = new List<Group>();
            for (int i = 0; i < groupCount;)
            {
                groups.Add(new Group("Tisch " + ++i));
            }

            for (int i = 0; i < teamCount; i++)
            {
                int grNr = i % groupCount;
                //int tNr = grNr * groupCount + groups[grNr].Teams.Count + 1;
                groups[grNr].Teams.Add(new Team() { Name = "Team " + (i + 1) });
            }

            for (int i = 0; i < groupCount; i++)
            {
                var g = groups[i];
                switch (g.Teams.Count)
                {
                    case 3:
                        {
                            int[,] m =
                            {
                                {1, 2},
                                {2, 3},
                                {3, 1}
                            };
                            for (int j = 0; j < m.Length / 2; j++)
                            {
                                g.Games.Add(new Game(g.Teams[m[j, left] - 1], g.Teams[m[j, right] - 1]));
                            }
                        }
                        break;

                    case 4:
                        {
                            int[,] m =
                             {
                                {2, 1},
                                {3, 4},
                                {4, 2},
                                {1, 3},
                                {4, 1},
                                {2, 3}
                            };
                            for (int j = 0; j < m.Length / 2; j++)
                            {
                                g.Games.Add(new Game(g.Teams[m[j, left] - 1], g.Teams[m[j, right] - 1]));
                            }
                        }
                        break;

                    case 5:
                        {
                            int[,] m =
                            {
                                {1, 4},
                                {2, 5},
                                {3, 1},
                                {4, 2},
                                {5, 3},
                                {1, 2},
                                {4, 5},
                                {3, 2},
                                {1, 5},
                                {4, 3}
                            };
                            for (int j = 0; j < m.Length / 2; j++)
                            {
                                g.Games.Add(new Game(g.Teams[m[j, left] - 1], g.Teams[m[j, right] - 1]));
                            }
                        }
                        break;
                }
            }

            new ControlWindow(new Tournament(groups)).Show();

            foreach (Group g in groups)
            {
                new GroupWindow() { DataContext = g }.Show();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}