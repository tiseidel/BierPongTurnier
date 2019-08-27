using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System;

namespace BierPongTurnier.Settings
{
    public class AddGameSetting : Setting
    {
        private Group _selectedGroup;

        private Team _selectedTeam1;

        private Team _selectedTeam2;

        public Group SelectedGroup
        {
            get => this._selectedGroup; set
            {
                this._selectedGroup = value;
                this.OnPropertyChanged();
                this.AddGameCommand.RaiseCanExecuteChanged();
            }
        }

        public Team SelectedTeam1
        {
            get => this._selectedTeam1; set
            {
                this._selectedTeam1 = value;
                this.OnPropertyChanged();
                this.AddGameCommand.RaiseCanExecuteChanged();
            }
        }

        public Team SelectedTeam2
        {
            get => this._selectedTeam2; set
            {
                this._selectedTeam2 = value;
                this.OnPropertyChanged();
                this.AddGameCommand.RaiseCanExecuteChanged();
            }
        }

        public Tournament Tournament { get; }

        public Command AddGameCommand { get; }

        public AddGameSetting(Tournament tournament)
        {
            this.Tournament = tournament;
            this.AddGameCommand = new Command(this.OnExecute, () => this.SelectedGroup != null && this.SelectedTeam1 != null && this.SelectedTeam2 != null && !Equals(this.SelectedTeam1, this.SelectedTeam2));
        }

        private void OnExecute()
        {
            try
            {
                var group = this.SelectedGroup;
                var t1 = this.SelectedTeam1;
                var t2 = this.SelectedTeam2;

                group.Games.Add(new Game(t1, t2));

                this.SelectedTeam1 = null;
                this.SelectedTeam2 = null;
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
    }
}