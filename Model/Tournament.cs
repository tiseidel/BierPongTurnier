using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BierPongTurnier.Model
{
    public class Tournament : BPTObject
    {
        public ObservableCollection<Group> Groups { get; }

        public string Name { get; internal set; }

        public Tournament(List<Group> groups) : base()
        {
            this.Groups = new ObservableCollection<Group>(groups);
        }
    }
}