using System;
using System.ComponentModel;

namespace BierPongTurnier.Model
{
    public interface IGame : INotifyPropertyChanged
    {
        Team Team1 { get; }

        Team Team2 { get; }

        int Beers1 { get; set; }

        int Beers2 { get; set; }

        TeamPosition WinnerPosition { get; }

        GameResult ResultForTeam(Team team);
    }

    public class Game : BPTObject, IGame
    {
        public static readonly int BEERS_NOT_SET = -1;

        private int _beers1;

        private int _beers2;

        public Team Team1 { get; }

        public Team Team2 { get; }

        public int Beers1
        {
            get => this._beers1;
            set
            {
                if (this._beers1 != value)
                {
                    this._beers1 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int Beers2
        {
            get => this._beers2;
            set
            {
                if (this._beers2 != value)
                {
                    this._beers2 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Game(Team team1, Team team2) : base()
        {
            this.Team1 = team1 ?? throw new ArgumentNullException(nameof(team1));
            this.Team2 = team2 ?? throw new ArgumentNullException(nameof(team2));
            if (Team.Equals(team1, team2)) throw new ArgumentException();

            this._beers1 = Game.BEERS_NOT_SET;
            this._beers2 = Game.BEERS_NOT_SET;
        }

        public Game(Guid id, Team team1, Team team2, int beers1, int beers2) : base(id)
        {
            this.Team1 = team1 ?? throw new ArgumentNullException(nameof(team1));
            this.Team2 = team2 ?? throw new ArgumentNullException(nameof(team2));
            if (Team.Equals(team1, team2)) throw new ArgumentException();

            this._beers1 = beers1;
            this._beers2 = beers2;
        }

        public bool IsValid => this.Beers1 != BEERS_NOT_SET && this.Beers2 != BEERS_NOT_SET;

        public TeamPosition WinnerPosition
        {
            get
            {
                if (!this.IsValid)
                {
                    return TeamPosition.NONE;
                }

                if (this.Beers1 < this.Beers2)
                {
                    return TeamPosition.FIRST;
                }
                else if (this.Beers2 < this.Beers1)
                {
                    return TeamPosition.SECOND;
                }
                else
                {
                    return TeamPosition.NONE;
                }
            }
        }

        public GameResult ResultForTeam(Team team)
        {
            if (!this.IsValid)
            {
                return GameResult.OPEN;
            }

            if (team.Equals(this.Team1))
            {
                if (this.Beers1 < this.Beers2)
                {
                    return GameResult.WIN;
                }
                else if (this.Beers1 > this.Beers2)
                {
                    return GameResult.LOSE;
                }
                else
                {
                    return GameResult.DRAW;
                }
            }
            else if (team.Equals(this.Team2))
            {
                if (this.Beers2 < this.Beers1)
                {
                    return GameResult.WIN;
                }
                else if (this.Beers2 > this.Beers1)
                {
                    return GameResult.LOSE;
                }
                else
                {
                    return GameResult.DRAW;
                }
            }
            else
            {
                return GameResult.NOT_PARTICIPATED;
            }
        }
    }
}