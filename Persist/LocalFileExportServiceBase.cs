using BierPongTurnier.Model;
using System;
using System.IO;

namespace BierPongTurnier.Persist
{
    public abstract class LocalFileExportServiceBase : IExportService
    {
        public string FileEnding { get; }

        protected LocalFileExportServiceBase(string fileEnding)
        {
            this.FileEnding = fileEnding ?? throw new ArgumentNullException(nameof(fileEnding));
        }

        public void Export(Tournament tournament, bool isAutosave)
        {
            var tournamentPath = this.CreateTournamentRootPath(tournament.Name);
            var fileName = this.CreateFileName(this.FileEnding, isAutosave);
            var content = this.CreateFileContent(tournament);

            this.PersistFile(tournamentPath, fileName, content);
        }

        protected abstract string CreateFileContent(Tournament tournament);

        protected string CreateFileName(string fileEnding, bool isAutoSave)
        {
            var filename = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            if (isAutoSave) filename += "_autosave";
            filename += "." + fileEnding;

            return filename;
        }

        protected string CreateTournamentRootPath(string tournamentName)
        {
            return Constants.DIR_SAVEFILES + "\\" + tournamentName;
        }

        private void CheckAndCreateTournamentDirectory(string tournamentPath)
        {
            var dir = tournamentPath;
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void PersistFile(string tournamentPath, string fileName, string fileContent)
        {
            this.CheckAndCreateTournamentDirectory(tournamentPath);
            var fullPath = Path.Combine(tournamentPath, fileName);

            try
            {
                System.IO.File.WriteAllText(fullPath, fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(fullPath);
                Console.WriteLine(ex);
            }
        }
    }
}