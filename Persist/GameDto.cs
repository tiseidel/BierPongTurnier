using BierPongTurnier.Model;
using System;
using System.Collections.Generic;

namespace BierPongTurnier.Persist
{
    internal class GameDto
    {
        public Guid Id { get; set; }

        public Guid Team1Id { get; set; }

        public int Team1Beers { get; set; }

        public int Team2Beers { get; set; }

        public Guid Team2Id { get; set; }

        public GameDto ToDto(Game game)
        {
            this.Id = game.Id;
            this.Team1Id = game.Team1.Id;
            this.Team2Id = game.Team2.Id;
            this.Team1Beers = game.Beers1;
            this.Team2Beers = game.Beers2;
            return this;
        }

        public Game Convert(List<Team> teams)
        {
            return new Game(teams.Find(t => t.Id.Equals(this.Team1Id)), teams.Find(t => t.Id.Equals(this.Team2Id)))
            {
                Id = this.Id,
                Beers1Input = Game.BEERS_NOT_SET.ToString().Equals(this.Team1Beers.ToString()) ? string.Empty : this.Team1Beers.ToString(),
                Beers2Input = Game.BEERS_NOT_SET.ToString().Equals(this.Team2Beers.ToString()) ? string.Empty : this.Team2Beers.ToString()
            };
        }
    }
}