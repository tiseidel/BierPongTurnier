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
            /*
            var configWindow = new ConfigurationWindow(this);
            configWindow.Show();
            */
            this.OnBeerPongConfiguration(1, 4);
        }

        public void OnBeerPongConfiguration(int groupCount, int teamCount)
        {
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
                            int[] l = { 1, 2, 3 };
                            int[] r = { 2, 3, 1 };

                            for (int j = 0; j < r.Length; j++)
                            {
                                g.Games.Add(new Game(g.Teams[l[j] - 1], g.Teams[r[j] - 1]));
                            }
                        }
                        break;

                    case 4:
                        {
                            int[] l = { 2, 3, 4, 1, 4, 2 };
                            int[] r = { 1, 4, 2, 3, 1, 3 };
                            for (int j = 0; j < r.Length; j++)
                            {
                                g.Games.Add(new Game(g.Teams[l[j] - 1], g.Teams[r[j] - 1]));
                            }
                        }
                        break;

                    case 5:
                        {
                            int[] l = { 1, 2, 3, 4, 5, 1, 4, 3, 1, 4 };
                            int[] r = { 4, 5, 1, 2, 3, 2, 5, 2, 5, 3 };
                            for (int j = 0; j < r.Length; j++)
                            {
                                g.Games.Add(new Game(g.Teams[l[j] - 1], g.Teams[r[j] - 1]));
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