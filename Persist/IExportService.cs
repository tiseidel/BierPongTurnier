using BierPongTurnier.Model;

namespace BierPongTurnier.Persist
{
    public interface IExportService
    {
        void Export(Tournament tournament, bool isAutosave);
    }
}