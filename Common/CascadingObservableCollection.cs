using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BierPongTurnier.Common
{
    public class CascadingObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public CascadingObservableCollection()
        {
            CollectionChanged += this.CascadingObservableCollectionChanged;
        }

        public CascadingObservableCollection(IEnumerable<T> pItems) : base(pItems)
        {
            CollectionChanged += this.CascadingObservableCollectionChanged;
        }

        private void CascadingObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (object item in e.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += this.ItemPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (object item in e.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= this.ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, this.IndexOf((T)sender)));
        }
    }
}