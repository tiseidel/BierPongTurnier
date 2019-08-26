﻿using BierPongTurnier.Model;
using System;
using System.Collections.Generic;

namespace BierPongTurnier.Persist
{
    internal class TournamentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<GroupDto> Groups { get; set; }

        public TournamentDto ToDto(Tournament tournament)
        {
            this.Groups = new List<GroupDto>();
            this.Name = tournament.FileName;
            foreach (Group g in tournament.Groups)
            {
                this.Groups.Add(new GroupDto().ToDto(g));
            }

            return this;
        }

        public Tournament Convert()
        {
            var list = new List<Group>();
            foreach (GroupDto g in this.Groups)
            {
                list.Add(g.Convert());
            }

            return new Tournament(list) { Id = this.Id, FileName = this.Name };
        }
    }
}