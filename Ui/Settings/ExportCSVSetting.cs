using BierPongTurnier.Model;
using BierPongTurnier.Persist;
using Newtonsoft.Json;
using System;
using System.Windows.Input;

namespace BierPongTurnier.Ui.Settings
{
    public class ExportCSVSetting : Setting
    {
        public ICommand ExportCommand { get; }

        public Tournament Tournament { get; }

        public ExportCSVSetting(Tournament tournament)
        {
            this.Tournament = tournament;
            this.ExportCommand = new Command(() => this.ToCSV(), () => true);

            this.ToCSV();
        }

        public void ToCSV(bool isAutoSave = false)
        {
            var groups = this.Tournament.Groups;
            var filename = this.Tournament.FileName;

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

            if (isAutoSave) filename += "_autosave";
            filename += "_" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            var path = @"C:\Users\timme\Desktop\BierPongTurnier\" + filename;

            try
            {
                System.IO.File.WriteAllText(path + ".csv", s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(path);
                Console.WriteLine(ex);
            }

            var dto = new TournamentDto().ToDto(this.Tournament);
            string output = JsonConvert.SerializeObject(dto);

            try
            {
                System.IO.File.WriteAllText(path + ".beer", output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(path);
                Console.WriteLine(ex);
            }
        }
    }
}