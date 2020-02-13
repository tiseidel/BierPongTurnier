using BierPongTurnier.Common;
using System.Collections.Generic;

namespace BierPongTurnier.Model
{
    public class BeerScore : BaseNotifyPropertyChanged
    {
        private int _beersGood;

        public int Good
        {
            get => this._beersGood; set
            {
                this._beersGood = value;
                this.OnPropertyChanged(nameof(BeerScore));
                this.OnPropertyChanged(nameof(this.Difference));
            }
        }

        private int _beersBad;

        public int Bad
        {
            get => this._beersBad; set
            {
                this._beersBad = value;
                this.OnPropertyChanged(nameof(BeerScore));
                this.OnPropertyChanged(nameof(this.Difference));
            }
        }

        public int Difference => this.Good - this.Bad;

        public override string ToString()
        {
            return $"{this.Good} : {this.Bad}";
        }
    }

    public class RankingEntry : BaseNotifyPropertyChanged
    {
        public Team Team { get; }

        public BeerScore BeerScore { get; }

        private int _games;

        public int Games
        {
            get => this._games; set
            {
                this._games = value;
                this.OnPropertyChanged();
            }
        }

        private int _points;

        public int Points
        {
            get => this._points; set
            {
                this._points = value;
                this.OnPropertyChanged();
            }
        }

        public RankingEntry(Team team) : base()
        {
            this.Team = team;
            this.BeerScore = new BeerScore();
        }

        public override bool Equals(object obj)
        {
            return obj is RankingEntry entry &&
                   EqualityComparer<Team>.Default.Equals(this.Team, entry.Team);
        }

        public override int GetHashCode()
        {
            return -1457990644 + EqualityComparer<Team>.Default.GetHashCode(this.Team);
        }
    }
}