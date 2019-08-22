﻿namespace BierPongTurnier
{
    public class Team : BPTObject
    {
        private string _name;

        public string Name
        {
            get => this._name; set
            {
                this._name = value;
                this.OnPropertyChanged();
            }
        }

        public Team(string name) : base()
        {
            this.Name = name;
        }
    }
}