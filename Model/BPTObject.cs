using BierPongTurnier.Common;
using System;
using System.Collections.Generic;

namespace BierPongTurnier.Model
{
    public class BPTObject : BaseNotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public BPTObject()
        {
            this.Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            return obj is BPTObject @object &&
                   this.Id.Equals(@object.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(this.Id);
        }
    }
}