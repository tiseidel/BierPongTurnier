using System;
using System.Collections.Generic;

namespace BierPongTurnier.Services
{
    public class TournamentRegistration
    {
        public int Id { get; set; }

        public DateTime Begin { get; set; }

        public int MaxTeams { get; set; }

        public List<TeamRegistration> Teams { get; set; }
    }
}