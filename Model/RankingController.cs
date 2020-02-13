using BierPongTurnier.Ui;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BierPongTurnier.Model
{
    public class RankingController
    {
        public Group Group { get; }

        public ObservableCollection<RankingEntry> Entries { get; }

        private bool _isCalculating = false;

        public IAutoSaveCallback AutoSave { get; set; }

        public RankingController(Group group)
        {
            this.Entries = new ObservableCollection<RankingEntry>();
            this.Group = group;
        }

        public void Calculate()
        {
            if (this._isCalculating)
            {
                return;
            }

            this._isCalculating = true;

            var list = this.CalculateScores();
            this.SortEntries(list);
            this.SetEntries(list);

            this.AutoSave?.DataChanged();
            this._isCalculating = false;
        }

        private List<RankingEntry> CalculateScores()
        {
            var list = new List<RankingEntry>(this.Entries);
            foreach (RankingEntry r in list)
            {
                var t = r.Team;
                int beersGood = 0;
                int beersBad = 0;
                int points = 0;
                int games = 0;
                foreach (Game g in this.Group.Games)
                {
                    var result = GameHelper.GetTeamResult(g, t);
                    switch (result)
                    {
                        case GameResult.WIN:
                            games++;
                            points += Constants.POINTS_WIN;
                            break;

                        case GameResult.DRAW:
                            games++;
                            points += Constants.POINTS_DRAW;
                            break;

                        case GameResult.LOSE:
                            games++;
                            break;

                        case GameResult.OPEN:
                        case GameResult.NOT_PARTICIPATED:
                            continue;
                    }
                    beersGood += GameHelper.GetGoodBeers(g, t);
                    beersBad += GameHelper.GetBadBeers(g, t);
                }
                r.BeerScore.Good = beersGood;
                r.BeerScore.Bad = beersBad;
                r.Points = points;
                r.Games = games;
            }
            return list;
        }

        private void SortEntries(List<RankingEntry> entries)
        {
            entries.Sort((l, r) =>
            {
                var c = l.Points.CompareTo(r.Points);
                if (c != 0)
                {
                    return -c;
                }
                c = l.BeerScore.Difference.CompareTo(r.BeerScore.Difference);
                if (c != 0)
                {
                    return -c;
                }
                c = l.BeerScore.Good.CompareTo(r.BeerScore.Good);
                if (c != 0)
                {
                    return -c;
                }

                return l.BeerScore.Bad.CompareTo(r.BeerScore.Bad);
            });
        }

        private void SetEntries(List<RankingEntry> entries)
        {
            this.Entries.Clear();
            foreach (RankingEntry r in entries)
            {
                this.Entries.Add(r);
            }
        }
    }
}