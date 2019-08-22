using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BierPongTurnier
{
    /// <summary>
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    ///
    public interface IAutoSaveCallback
    {
        void DataChanged();
    }

    public partial class ControlWindow : Window, INotifyPropertyChanged, IAutoSaveCallback
    {
        private Group _selectedGroup;

        private Team _selectedTeam1;

        private Team _selectedTeam2;

        public Group SelectedGroup
        {
            get => this._selectedGroup; set
            {
                this._selectedGroup = value;
                this.OnPropertyChanged();
            }
        }

        public Team SelectedTeam1
        {
            get => this._selectedTeam1; set
            {
                this._selectedTeam1 = value;
                this.OnPropertyChanged();
            }
        }

        public Team SelectedTeam2
        {
            get => this._selectedTeam2; set
            {
                this._selectedTeam2 = value;
                this.OnPropertyChanged();
            }
        }

        public List<Group> Groups { get; }

        public ControlWindow(List<Group> groups)
        {
            this.InitializeComponent();
            this.Groups = groups;
            this.ToCSV("Turnier_4");

            this.DataContext = this;

            foreach (Group g in groups)
            {
                //Todo use event
                g.Ranking.AutoSave = this;
            }
        }

        public void ToCSV(string filename)
        {
            var groups = this.Groups;

            if (groups.Count == 0) return;

            string empty = ";;;;";
            string divider = ";";

            int gc = groups.Count;
            int maxT = 0;
            int maxG = 0;
            foreach (Group g in groups)
            {
                if (g.Teams.Count > maxT)
                {
                    maxT = g.Teams.Count;
                }

                if (g.Games.Count > maxG)
                {
                    maxG = g.Games.Count;
                }
            }

            string s = "";

            for (int i = 0; i < gc; i++)
            {
                s += groups[i].Name + empty + divider;
            }

            s += "\n";
            for (int i = 0; i < gc; i++)
            {
                s += divider + empty;
            }
            s += "\n";

            for (int t = 0; t < maxT; t++)
            {
                for (int g = 0; g < gc; g++)
                {
                    var group = groups[g];
                    if (group.Teams.Count > t)
                    {
                        var ranking = group.Ranking.Entries[t];
                        s += $"{ranking.Team.Name};{ranking.BeerScore.Good}:{ranking.BeerScore.Bad};{ranking.Points};;" + divider;
                    }
                    else
                    {
                        s += empty + divider;
                    }
                }
                s += "\n";
            }

            for (int i = 0; i < gc; i++)
            {
                s += divider + empty;
            }
            s += "\n";

            for (int m = 0; m < maxG; m++)
            {
                for (int g = 0; g < gc; g++)
                {
                    var group = groups[g];
                    if (group.Games.Count > m)
                    {
                        var game = group.Games[m];
                        s += $"{game.Team1.Name};{(game.Beers1 != -1 ? (game.Beers1 + "") : "")};{(game.Beers2 != -1 ? (game.Beers2 + "") : "")};{game.Team2.Name};" + divider;
                    }
                    else
                    {
                        s += empty + divider;
                    }
                }
                s += "\n";
            }

            filename += "_" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            var path = @"C:\Users\timme\Desktop\BierPongTurnier\" + filename + ".csv";
            try
            {
                System.IO.File.WriteAllText(path, s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(path);
                Console.WriteLine(ex);
            }
        }

        private void Button_ExportCSV_Click(object sender, RoutedEventArgs e)
        {
            this.ToCSV("Turnier_4");
        }

        private void Button_AddGame_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedGroup == null || this.SelectedTeam1 == null || this.SelectedTeam2 == null || Team.Equals(this.SelectedTeam1, this.SelectedTeam2))
            {
                return;
            }

            try
            {
                var group = this.SelectedGroup;
                var t1 = this.SelectedTeam1;
                var t2 = this.SelectedTeam2;

                group.Games.Add(new Game(t1, t2));

                this.SelectedGroup = null;
                this.SelectedTeam1 = null;
                this.SelectedTeam2 = null;
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private int closeCount = 0;

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = this.closeCount++ < 5;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DataChanged()
        {
            this.ToCSV("Turnier_4_autosave");
        }
    }
}