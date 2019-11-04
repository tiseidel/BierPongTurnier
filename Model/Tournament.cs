using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BierPongTurnier.Model
{
    public class Tournament : BPTObject
    {
        public string Name { get; set; }

        public ObservableCollection<Group> Groups { get; }

        public Tournament(List<Group> groups) : base()
        {
            this.Groups = new ObservableCollection<Group>(groups);
        }
    }
}