using BierPongTurnier.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Group = BierPongTurnier.Model.Group;

namespace BierPongTurnier.Ui.Modes
{
    public partial class RandomPlayerModeControl : UserControl, INotifyPropertyChanged
    {
        private string _input;

        public string Input
        {
            get => this._input;
            set
            {
                this._input = value;
                this.ParseInput(value);
                this.OnPropertyChanged();
            }
        }

        private int _count;

        public int Count
        {
            get => this._count;
            set
            {
                this._count = value;
                this.OnPropertyChanged();
            }
        }

        private void ParseInput(string s)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            s = regex.Replace(s, " ");

            s = s.Replace(",", "\n");

            this.Count = s.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Count();
        }

        private List<string> Convert()
        {
            var s = this._input;

            if (string.IsNullOrWhiteSpace(s))
            {
                return new List<string>();
            }

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            s = regex.Replace(s, " ");

            s = s.Replace(",", "\r");

            var list = s.Split('\r').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            }
            return list;
        }

        private Random rng = new Random();

        public void Shuffle(IList<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public RandomPlayerModeControl()
        {
            this.InitializeComponent();

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var list = this.Convert();
            for (int i = 0; i < 42; i++)
                this.Shuffle(list);
            var groups = Creator.FromPlayers(list, 2);

            new ControlWindow(new Tournament(groups)).Show();

            foreach (Group g in groups)
            {
                new GroupWindow() { DataContext = g }.Show();
            }
        }
    }
}