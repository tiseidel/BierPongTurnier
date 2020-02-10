using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BierPongTurnier.Services
{
    public class MockTournamentRegistrationService : ITournamentRegistrationService
    {
        private readonly List<TournamentRegistration> _mock = new List<TournamentRegistration>()
        {
            new TournamentRegistration()
            {
                Id = 0,
                Begin = DateTime.Today,
                Teams = GenerateTeamRegistrations(16)
            },
            new TournamentRegistration()
            {
                Id = 1,
                Begin = DateTime.Today - TimeSpan.FromDays(55),
                Teams = GenerateTeamRegistrations(18)
            },
            new TournamentRegistration()
            {
                Id = 2,
                Begin = DateTime.Today - TimeSpan.FromDays(59),
                Teams = GenerateTeamRegistrations(32)
            },
              new TournamentRegistration()
            {
                Id = 3,
                Begin = DateTime.Today - TimeSpan.FromDays(62),
                Teams = GenerateTeamRegistrations(40)
            }
        };

        public async Task<List<int>> GetTournamentRegistrationIds()
        {
            await Task.Delay(1000);
            var list = new List<int>();
            foreach (var t in this._mock)
            {
                list.Add(t.Id);
            }
            return list;
        }

        public async Task<TournamentRegistration> GetTournamentRegistration(int id)
        {
            await Task.Delay(250);
            return this._mock.Find(t => t.Id == id);
        }

        private static List<TeamRegistration> GenerateTeamRegistrations(int count)
        {
            var list = new List<TeamRegistration>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new TeamRegistration()
                {
                    Id = i,
                    Player1Name = "Spieler " + (i * 2 + 1),
                    Player2Name = "Spieler " + (i * 2 + 2),
                });
            }
            return list;
        }
    }
}