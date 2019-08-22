using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BierPongTurnier
{
    public class RankingController
    {
        public Group Group { get; }

        public ObservableCollection<RankingEntry> Entries { get; }

        private bool _lock = false;

        public IAutoSaveCallback AutoSave { get; set; }

        public RankingController(Group group)
        {
            this.Entries = new ObservableCollection<RankingEntry>();
            this.Group = group;
        }

        public void Calculate()
        {
            if (this._lock)
            {
                return;
            }

            this._lock = true;

            foreach (RankingEntry r in this.Entries)
            {
                var t = r.Team;
                int beersGood = 0;
                int beersBad = 0;
                int points = 0;
                foreach (Game g in this.Group.Games)
                {
                    var result = g.GetResult(t);
                    switch (result)
                    {
                        case GameResult.WIN:
                            points += Constants.POINTS_WIN;
                            break;

                        case GameResult.DRAW:
                            points += Constants.POINTS_DRAW;
                            break;

                        case GameResult.OPEN:
                        case GameResult.NOT_PARTICIPATED:
                            continue;
                    }
                    beersGood += this.GetGoodBeers(g, t);
                    beersBad += this.GetBadBeers(g, t);

                  
                }
                r.BeerScore.Good = beersGood;
                r.BeerScore.Bad = beersBad;
                r.Points = points;
            }
            var list = new List<RankingEntry>(this.Entries);
            list.Sort((l, r) => this.CompareStats(l, r));
            this.Entries.Clear();
            foreach (RankingEntry r in list)
            {
                this.Entries.Add(r);
            }
            this.AutoSave?.DataChanged();
            this._lock = false;
        }

        private int CompareStats(RankingEntry l, RankingEntry r)
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
        }

        private int GetGoodBeers(Game g, Team t)
        {
            if (t.Equals(g.Team1))
            {
                return g.Beers2 != -1 ? g.Beers2 : 0;
            }
            else if (t.Equals(g.Team2))
            {
                return g.Beers1 != -1 ? g.Beers1 : 0;
            }
            else
            {
                return 0;
            }
        }

        private int GetBadBeers(Game g, Team t)
        {
            if (t.Equals(g.Team1))
            {
                return g.Beers1 != -1 ? g.Beers1 : 0;
            }
            else if (t.Equals(g.Team2))
            {
                return g.Beers2 != -1 ? g.Beers2 : 0;
            }
            else
            {
                return 0;
            }
        }
    }
}