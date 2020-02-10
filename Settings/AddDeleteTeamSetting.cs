using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System.Linq;
using System.Windows;

namespace BierPongTurnier.Settings
{
    public class AddDeleteTeamSetting : Setting
    {
        private string _name;

        private Group _selectedGroup;

        private string _commandText;

        public Tournament Tournament { get; }

        public Group SelectedGroup
        {
            get => this._selectedGroup;
            set
            {
                this._selectedGroup = value;
                this.OnPropertyChanged();
            }
        }

        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                this.OnPropertyChanged();
                this.CheckCommandText();
            }
        }

        public string CommandText
        {
            get => this._commandText;
            set
            {
                this._commandText = value;
                this.OnPropertyChanged();
            }
        }

        public void CheckCommandText()
        {
            if (string.IsNullOrWhiteSpace(this._name))
            {
                this.CommandText = "Add/Del";
            }
            else if (this.HasTeam(this._selectedGroup, this._name))
            {
                this.CommandText = "Delete";
            }
            else
            {
                this.CommandText = "Add";
            }
        }

        public Command AddDeleteTeamCommand { get; }

        public AddDeleteTeamSetting(Tournament tournament)
        {
            this.Tournament = tournament;
            this.AddDeleteTeamCommand = new Command(() => this.AddDeleteTeam(), () => this.SelectedGroup != null && !string.IsNullOrWhiteSpace(this._name));
            this._commandText = "Add/Del";
        }

        private void AddDeleteTeam()
        {
            if (this.HasStarted()) return;
            if (string.IsNullOrWhiteSpace(this._name)) return;

            if (this.HasTeam(this._selectedGroup, this._name))
            {
                this.DeleteTeam();
            }
            else
            {
                this.AddTeam();
            }
            TournamentCreator.CreateRoundRobin(this._selectedGroup);
            this.Name = "";
        }

        private void DeleteTeam()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Soll das Team wirklich gelöscht werden?", "Team löschen?", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Team t = this.SelectedGroup.Teams.First(o => o.Name.Equals(this._name));
                this._selectedGroup.Teams.Remove(t);
            }
        }

        private void AddTeam()
        {
            this._selectedGroup.Teams.Add(new Team() { Name = this._name });
        }

        private bool HasTeam(Group g, string str)
        {
            return g != null && g.Teams.Where(t => t.Name.Equals(str)).Any();
        }

        private bool HasStarted()
        {
            foreach (Game g in this._selectedGroup.Games)
            {
                if (g.IsValid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}