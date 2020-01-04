using BierPongTurnier.Common;
using BierPongTurnier.Model;
using BierPongTurnier.Persist;
using System.Collections.Generic;
using System.Windows.Input;

namespace BierPongTurnier.Settings
{
    public class QuickSaveSetting : Setting
    {
        private readonly List<IExportService> _exportServices = new List<IExportService>()
        {
            new CsvExportService(),
            new JsonService()
        };

        public Tournament Tournament { get; }

        public ICommand ExportCommand { get; }

        public QuickSaveSetting(Tournament tournament)
        {
            this.Tournament = tournament;
            this.ExportCommand = new Command(() => this.Export(false));
        }

        public void Export(bool isAutoSave)
        {
            foreach (IExportService exportService in this._exportServices)
            {
                exportService.Export(this.Tournament, isAutoSave);
            }
        }
    }
}