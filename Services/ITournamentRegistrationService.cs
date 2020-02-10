using System.Collections.Generic;
using System.Threading.Tasks;

namespace BierPongTurnier.Services
{
    public interface ITournamentRegistrationService
    {
        Task<List<int>> GetTournamentRegistrationIds();

        Task<TournamentRegistration> GetTournamentRegistration(int id);
    }
}