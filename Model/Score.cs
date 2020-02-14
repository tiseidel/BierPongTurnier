using BierPongTurnier.Common;

namespace BierPongTurnier.Model
{
    public class Score : BaseNotifyPropertyChanged
    {
        public static readonly int BEERS_NOT_SET = -1;

        private int _beers1 = BEERS_NOT_SET;

        public int Beers1
        {
            get => this._beers1;
            set
            {
                if (this._beers1 != value)
                {
                    this._beers1 = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private int _beers2 = BEERS_NOT_SET;

        public int Beers2
        {
            get => this._beers2;
            set
            {
                if (this._beers2 != value)
                {
                    this._beers2 = value;
                    this.OnPropertyChanged();
                }
            }
        }



        public bool IsValid => this.Beers1 != BEERS_NOT_SET && this.Beers2 != BEERS_NOT_SET;

        public override bool Equals(object obj)
        {
            return obj is Score score &&
                   this._beers1 == score._beers1 &&
                   this._beers2 == score._beers2;
        }
    }
}