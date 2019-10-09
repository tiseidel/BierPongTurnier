using System;
using System.Windows;

namespace BierPongTurnier.Model
{
    public class Game : BPTObject
    {
        public static readonly int BEERS_NOT_SET = -1;

        private string _beers1Input;

        private string _beers2Input;

        private TeamPosition _winnerPosition;

        public Team Team1 { get; }

        public Team Team2 { get; }

        public string Beers1Input
        {
            get => this._beers1Input;
            set
            {
                this._beers1Input = value;
                this.OnPropertyChanged();
                this.CalculateWinnerPosition();
            }
        }

        public string Beers2Input
        {
            get => this._beers2Input;
            set
            {
                this._beers2Input = value;
                this.OnPropertyChanged();
                this.CalculateWinnerPosition();
            }
        }

        public int Beers1
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._beers1Input))
                {
                    return BEERS_NOT_SET;
                }
                else
                {
                    try
                    {
                        return int.Parse(this._beers1Input);
                    }
                    catch (Exception)
                    {
                        return BEERS_NOT_SET;
                    }
                }
            }
        }

        public int Beers2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._beers2Input))
                {
                    return BEERS_NOT_SET;
                }
                else
                {
                    try
                    {
                        return int.Parse(this._beers2Input);
                    }
                    catch (Exception)
                    {
                        return BEERS_NOT_SET;
                    }
                }
            }
        }

        public TeamPosition WinnerPosition
        {
            get => this._winnerPosition;
            set
            {
                this._winnerPosition = value;
                this.OnPropertyChanged();
            }
        }

        public Game(Team team1, Team team2) : base()
        {
            this.Team1 = team1 ?? throw new ArgumentNullException(nameof(team1));
            this.Team2 = team2 ?? throw new ArgumentNullException(nameof(team2));
            if (Team.Equals(team1, team2)) throw new ArgumentException();

            this._winnerPosition = TeamPosition.NONE;
        }

        public bool IsValid => this.Beers1 != BEERS_NOT_SET && this.Beers2 != BEERS_NOT_SET;

        public void CalculateWinnerPosition()
        {
            if (!this.IsValid)
            {
                this.WinnerPosition = TeamPosition.NONE;
                return;
            }

            if (this.Beers1 < this.Beers2)
            {
                this.WinnerPosition = TeamPosition.FIRST;
            }
            else if (this.Beers2 < this.Beers1)
            {
                this.WinnerPosition = TeamPosition.SECOND;
            }
            else
            {
                this.WinnerPosition = TeamPosition.NONE;
            }
        }

        public GameResult GetResult(Team team)
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