using BierPongTurnier.Model;
using System;

namespace BierPongTurnier.Persist.Dto
{
    internal class TeamDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public TeamDto ToDto(Team team)
        {
            this.Id = team.Id;
            this.Name = team.Name;
            return this;
        }

        public Team Convert()
        {
            return new Team() { Id = this.Id, Name = this.Name };
        }
    }
}