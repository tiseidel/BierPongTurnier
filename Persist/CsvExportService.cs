using BierPongTurnier.Model;

namespace BierPongTurnier.Persist
{
    public class CsvExportService : LocalFileExportServiceBase
    {
        public CsvExportService() : base("csv")
        {
        }

        protected override string CreateFileContent(Tournament tournament)
        {
            var groups = tournament.Groups;

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
                        s += $"{game.Team1.Name};{(game.Score.Beers1 != Score.BEERS_NOT_SET ? game.Score.Beers1 + "" : "")};{(game.Score.Beers2 != Score.BEERS_NOT_SET ? game.Score.Beers2 + "" : "")};{game.Team2.Name};" + divider;
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
    }
}