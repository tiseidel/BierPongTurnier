using BierPongTurnier.Model;
using BierPongTurnier.Persist.Dto;
using Newtonsoft.Json;
using System.IO;

namespace BierPongTurnier.Persist
{
    public class JsonService : LocalFileExportServiceBase, IImportService
    {
        public JsonService() : base("json")
        {
        }

        public Tournament Import(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TournamentDto>(json).Convert();
        }

        protected override string CreateFileContent(Tournament tournament)
        {
            return JsonConvert.SerializeObject(new TournamentDto().ToDto(tournament));
        }
    }
}