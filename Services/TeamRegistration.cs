using Newtonsoft.Json;

namespace BierPongTurnier.Services
{
    public class TeamRegistration
    {
        public int Id { get; set; }

        public string Player1Name { get; set; }

        [JsonProperty("player1external")]
        public bool IsPlayer1Extern { get; set; }

        public string Player2Name { get; set; }

        [JsonProperty("player2external")]
        public bool IsPlayer2Extern { get; set; }
    }
}