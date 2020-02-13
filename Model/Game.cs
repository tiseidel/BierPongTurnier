using System;
using System.ComponentModel;

namespace BierPongTurnier.Model
{
    public interface IGame : INotifyPropertyChanged
    {
        Team Team1 { get; }

        Team Team2 { get; }

        Score Score { get; }

        TeamPosition WinnerPosition { get; }
    }

    public class Game : BPTObject, IGame
    {
        public Team Team1 { get; }

        public Team Team2 { get; }

        public Score Score { get; }

        public Game(Team team1, Team team2) : base()
        {
            this.Team1 = team1 ?? throw new ArgumentNullException(nameof(team1));
            this.Team2 = team2 ?? throw new ArgumentNullException(nameof(team2));
            if (Team.Equals(team1, team2)) throw new ArgumentException();

            this.Score = new Score();
            this.Score.PropertyChanged += (s, e) => this.OnPropertyChanged(nameof(this.Score));
        }

        public Game(Guid id, Team team1, Team team2, int beers1, int beers2) : base(id)
        {
            this.Team1 = team1 ?? throw new ArgumentNullException(nameof(team1));
            this.Team2 = team2 ?? throw new ArgumentNullException(nameof(team2));
            if (Team.Equals(team1, team2)) throw new ArgumentException();

            this.Score = new Score()
            {
                Beers1 = beers1,
                Beers2 = beers2
            };
            this.Score.PropertyChanged += (s, e) => this.OnPropertyChanged(nameof(this.Score));
        }

        public TeamPosition WinnerPosition
        {
            get
            {
                if (!this.Score.IsValid)
                {
                    return TeamPosition.NONE;
                }

                if (this.Score.Beers1 < this.Score.Beers2)
                {
                    return TeamPosition.FIRST;
                }
                else if (this.Score.Beers2 < this.Score.Beers1)
                {
                    return TeamPosition.SECOND;
                }
                else
                {
                    return TeamPosition.NONE;
                }
            }
        }
    }
}