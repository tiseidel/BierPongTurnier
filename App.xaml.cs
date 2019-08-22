#define SKIP_CREATION_FOR_TESTING

using BierPongTurnier.Model;
using BierPongTurnier.Ui;
using System.Collections.Generic;
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

#if SKIP_CREATION_FOR_TESTING
            this.OnBeerPongConfiguration(1, 4);
#else
            var configWindow = new ConfigurationWindow(this);
            configWindow.Show();
#endif
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
                groups[grNr].Teams.Add(new Team("Team " + (i + 1)));
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
                            for (int j = 0; j < m.Length/2; j++)
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

            new ControlWindow(groups).Show();

            foreach (Group g in groups)
            {
                new GroupWindow() { DataContext = g }.Show();
            }
        }
    }
}