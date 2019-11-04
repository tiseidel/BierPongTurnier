using BierPongTurnier.Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BierPongTurnier.Model
{
    public class Group : BPTObject
    {
        public string Name { get; set; }

        public ObservableCollection<Team> Teams { get; }

        public CascadingObservableCollection<Game> Games { get; }

        public RankingController Ranking { get; }

        public Group() : base()
        {
            this.Teams = new ObservableCollection<Team>();
            this.Games = new CascadingObservableCollection<Game>();
            this.Ranking = new RankingController(this);

            this.Teams.CollectionChanged += (o, e) => this.Ranking.Calculate();
            this.Games.CollectionChanged += this.Teams_CollectionChanged;
        }

        private void Teams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Team item in e.NewItems)
                {
                    this.Ranking.Entries.Add(new RankingEntry(item));
                }
            }

            if (e.OldItems != null)
            {
                foreach (Team item in e.OldItems)
                {
                    RankingEntry r = null;
                    foreach (RankingEntry entry in this.Ranking.Entries)
                    {
                        if (entry.Team.Equals(item))
                        {
                            r = entry;
                        }
                    }
                    this.Ranking.Entries.Remove(r);
                }
            }
        }
    }
}