using BierPongTurnier.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BierPongTurnier.Persist
{
    internal class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<TeamDto> Teams { get; set; }

        public List<GameDto> Games { get; set; }

        public GroupDto ToDto(Group group)
        {
            this.Teams = new List<TeamDto>();
            this.Games = new List<GameDto>();

            this.Id = group.Id;
            this.Name = group.Name;

            foreach (Team t in group.Teams)
            {
                this.Teams.Add(new TeamDto().ToDto(t));
            }

            foreach (Game g in group.Games)
            {
                this.Games.Add(new GameDto().ToDto(g));
            }

            return this;
        }

        public Group Convert()
        {
            var group = new Group() { Id = this.Id, Name = this.Name };

            foreach (TeamDto t in this.Teams)
            {
                group.Teams.Add(t.Convert());
            }

            foreach (GameDto g in this.Games)
            {
                group.Games.Add(g.Convert(group.Teams.ToList()));
            }

            return group;
        }
    }
}