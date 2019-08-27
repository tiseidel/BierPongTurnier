using BierPongTurnier.Common;
using BierPongTurnier.Model;
using BierPongTurnier.Persist;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Input;

namespace BierPongTurnier.Settings
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
            this.CheckAndCreateTournamentDirectory();

            var filename = this.CreateFilePathWithoutType(isAutoSave);
            var path = Constants.DIR_SAVEFILES + "\\" + this.Tournament.FileName + "\\" + filename;

            try
            {
                string csvContent = this.CreateCSVContent();
                System.IO.File.WriteAllText(path + ".csv", csvContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(path);
                Console.WriteLine(ex);
            }

            try
            {
                string jsonContent = this.CreateJSONContent();
                System.IO.File.WriteAllText(path + ".beer", jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(path);
                Console.WriteLine(ex);
            }
        }

        private string CreateFilePathWithoutType(bool isAutoSave)
        {
            var filename = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            if (isAutoSave) filename += "_autosave";

            return filename;
        }

        private string CreateJSONContent()
        {
            return JsonConvert.SerializeObject(new TournamentDto().ToDto(this.Tournament));
        }

        private string CreateCSVContent()
        {
            var groups = this.Tournament.Groups;

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
                        s += $"{game.Team1.Name};{(game.Beers1 != -1 ? game.Beers1 + "" : "")};{(game.Beers2 != -1 ? game.Beers2 + "" : "")};{game.Team2.Name};" + divider;
                    }
                    else
                    {
                        s += empty + divider;
                    }
                }
                s += "\n";
            }

            return s;
        }

        private void CheckAndCreateTournamentDirectory()
        {
            var dir = Constants.DIR_SAVEFILES + "\\" + this.Tournament.FileName;
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}