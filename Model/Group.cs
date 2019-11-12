using BierPongTurnier.Common;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

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

            this.Games.CollectionChanged += (s, e) => this.Update();
            this.Teams.CollectionChanged += this.Teams_CollectionChanged;
        }

        public void Update()
        {
            this.Ranking.Calculate();
        }

        private void Teams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Ranking.Entries.Clear();
                this.Update();
                return;
            }

            var changed = false;
            if (e.NewItems != null)
            {
                foreach (Team item in e.NewItems)
                {
                    this.Ranking.Entries.Add(new RankingEntry(item));
                    changed = true;
                }
            }

            if (e.OldItems != null)
            {
                foreach (Team item in e.OldItems)
                {
                    this.Ranking.Entries.Remove(this.Ranking.Entries.First(r => r.Team.Equals(item)));
                    changed = true;
                }
            }

            if (changed)
            {
                this.Update();
            }
        }
    }
}