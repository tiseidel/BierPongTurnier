using BierPongTurnier.Common;
using System.Collections.ObjectModel;

namespace BierPongTurnier.Model
{
    public class Group : BPTObject
    {
        public ObservableCollection<Team> Teams { get; }

        public CascadingObservableCollection<Game> Games { get; }

        public RankingController Ranking { get; }

        public string Name { get; set; }

        public Group() : base()
        {
            this.Teams = new ObservableCollection<Team>();
            this.Games = new CascadingObservableCollection<Game>();
            this.Ranking = new RankingController(this);

            this.Teams.CollectionChanged += this.Teams_CollectionChanged;
            this.Games.CollectionChanged += this.Games_CollectionChanged;
        }

        private void Games_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Ranking.Calculate();
        }

        private void Teams_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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