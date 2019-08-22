namespace BierPongTurnier.Model
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

        public Team() : base()
        {
        }
    }
}