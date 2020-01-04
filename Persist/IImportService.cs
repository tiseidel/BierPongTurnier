using BierPongTurnier.Model;

namespace BierPongTurnier.Persist
{
    public interface IImportService
    {
        Tournament Import(string path);
    }
}