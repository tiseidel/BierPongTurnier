using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BierPongTurnier.Services
{
    public class TournamentRegistrationService : ITournamentRegistrationService
    {
        private readonly HttpClient _client;

        private readonly string _baseAddress = "http://turnier.debbrecht.com/api/v1";

        public TournamentRegistrationService()
        {
            this._client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public async Task<TournamentRegistration> GetTournamentRegistration(int id)
        {
            var response = await this._client.GetAsync($"{this._baseAddress}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);
            try
            {
                return JsonConvert.DeserializeObject<TournamentRegistration>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<int>> GetTournamentRegistrationIds()
        {
            var response = await this._client.GetAsync($"{this._baseAddress}/");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);
            try
            {
                return JsonConvert.DeserializeObject<List<int>>(content);
            }
            catch (Exception)
            {
                return new List<int>();
            }
        }
    }
}