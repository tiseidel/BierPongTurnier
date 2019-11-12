using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System;
using System.Windows;

namespace BierPongTurnier.Ui
{
    public class GameViewModel : BaseNotifyPropertyChanged, IGame
    {
        private FontWeight _team1Font;

        private FontWeight _team2Font;

        private string _beers1Input;

        private string _beers2Input;

        public string Beers1Input
        {
            get => this._beers1Input;
            set
            {
                this._beers1Input = value;
                this.OnPropertyChanged();
                try
                {
                    this.Game.Beers1 = int.Parse(value);
                }
                catch (Exception)
                {
                    this.Game.Beers1 = Game.BEERS_NOT_SET;
                }
            }
        }

        public string Beers2Input
        {
            get => this._beers2Input;
            set
            {
                this._beers2Input = value;
                this.OnPropertyChanged();
                try
                {
                    this.Game.Beers2 = int.Parse(value);
                }
                catch (Exception)
                {
                    this.Game.Beers2 = Game.BEERS_NOT_SET;
                }
            }
        }

        public FontWeight Team1Font
        {
            get => this._team1Font;
            set
            {
                this._team1Font = value;
                this.OnPropertyChanged();
            }
        }

        public FontWeight Team2Font
        {
            get => this._team2Font;
            set
            {
                this._team2Font = value;
                this.OnPropertyChanged();
            }
        }

        public Game Game { get; }

        public Team Team1 => this.Game.Team1;

        public Team Team2 => this.Game.Team2;

        public int Beers1
        {
            get => this.Game.Beers1;
            set
            {
                this.Game.Beers1 = value;
            }
        }

        public int Beers2
        {
            get => this.Game.Beers2;
            set
            {
                this.Game.Beers2 = value;
            }
        }

        public TeamPosition WinnerPosition => this.Game.WinnerPosition;

        public GameViewModel(Game game)
        {
            this.Game = game;

            this._team1Font = FontWeights.Normal;
            this._team2Font = FontWeights.Normal;

            this._beers1Input = game.Beers1 == Game.BEERS_NOT_SET ? "" : game.Beers1.ToString();
            this._beers2Input = game.Beers2 == Game.BEERS_NOT_SET ? "" : game.Beers2.ToString();

            this.Game.PropertyChanged += this.Game_PropertyChanged;
        }

        private void Game_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(this.Game.Beers1)))
            {
                this.UpdateFonts();
            }
            else if (e.PropertyName.Equals(nameof(this.Game.Beers2)))
            {
                this.UpdateFonts();
            }
        }

        public void UpdateFonts()
        {
            this.Team1Font = this.Game.WinnerPosition == TeamPosition.FIRST ? FontWeights.Bold : FontWeights.Normal;
            this.Team2Font = this.Game.WinnerPosition == TeamPosition.SECOND ? FontWeights.Bold : FontWeights.Normal;
        }

        public GameResult ResultForTeam(Team team) => this.Game.ResultForTeam(team);
    }
}