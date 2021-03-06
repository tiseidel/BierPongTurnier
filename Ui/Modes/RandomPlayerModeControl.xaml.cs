﻿using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BierPongTurnier.Ui.Modes
{
    public partial class RandomPlayerModeControl : UserControl, INotifyPropertyChanged, ITournamentStartMode
    {
        private readonly Random random = new Random();

        private string _tournamentName;

        public string TournamentName
        {
            get => this._tournamentName;
            set
            {
                this._tournamentName = value;
                this.OnPropertyChanged();
            }
        }

        private string _playerInput;

        public string PlayerInput
        {
            get => this._playerInput;
            set
            {
                this._playerInput = value;
                this.ParseInput(value);
                this.OnPropertyChanged();
            }
        }

        private string _groupCount;

        public string GroupCount
        {
            get => this._groupCount;
            set
            {
                this._groupCount = value;
                this.OnPropertyChanged();
            }
        }

        private int _playerCount;

        public int PlayerCount
        {
            get => this._playerCount;
            set
            {
                this._playerCount = value;
                this.OnPropertyChanged();
            }
        }

        public IStartTournamentCallback StartTournamentCallback { get; set; }

        public Command StartCommand { get; }

        public RandomPlayerModeControl()
        {
            this.InitializeComponent();

            this.StartCommand = new Command(this.Start, this.CanStart);

            this.DataContext = this;
        }

        private bool CanStart()
        {
            try
            {
                var gc = int.Parse(this._groupCount);
                return !string.IsNullOrWhiteSpace(this.TournamentName) && this.PlayerCount >= gc * 4;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Start()
        {
            if (!this.CanStart())
            {
                return;
            }
            var gc = int.Parse(this._groupCount);

            var list = this.Convert();
            for (int i = 0; i < 42; i++)
                Extensions.Shuffle(list, this.random);
            var groups = TournamentCreator.FromPlayers(list, gc);

            this?.StartTournamentCallback.Start(new Tournament(groups)
            {
                Name = this.TournamentName
            }, true);
        }

        private void ParseInput(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                this.PlayerCount = 0;
                return;
            }

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            s = regex.Replace(s, " ");

            s = s.Replace(",", "\n");

            this.PlayerCount = s.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Count();
        }

        private List<string> Convert()
        {
            var s = this._playerInput;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}