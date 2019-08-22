using System.Collections.ObjectModel;

namespace BierPongTurnier.Model
{
    public class Tournament : BPTObject
    {
        public ObservableCollection<Group> Groups { get; }

        public Tournament() : base()
        {
            this.Groups = new ObservableCollection<Group>();
        }
    }
}