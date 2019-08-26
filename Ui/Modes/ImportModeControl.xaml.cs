using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace BierPongTurnier.Ui.Modes
{
    public partial class ImportModeControl : UserControl
    {
        public Command StartCommand { get; }

        public ImportModeControl()
        {
            this.InitializeComponent();

            this.StartCommand = new Command(this.Start);

            this.DataContext = this;
        }

        private void Start()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.ParseFile(File.ReadAllText(openFileDialog.FileName));
            }
        }

        private void ParseFile(string file)
        {
        }
    }
}