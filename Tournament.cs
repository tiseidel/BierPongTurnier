using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BierPongTurnier
{
    public class Tournament : BPTObject
    {
        public ObservableCollection<Group> Groups {get;}

        public Tournament() : base()
        {
            this.Groups = new ObservableCollection<Group>();
        }
    }
}