using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace BierPongTurnier.Ui
{
    public class GroupViewModel : BaseNotifyPropertyChanged
    {
        public Group Group { get; }

        public string Name => this.Group.Name;

        public ObservableCollection<GameViewModel> GameViewModels { get; }

        public GroupViewModel(Group group)
        {
            this.Group = group ?? throw new ArgumentNullException(nameof(group));
            this.GameViewModels = new ObservableCollection<GameViewModel>();

            this.ConvertGames(group.Games);
            this.Group.Games.CollectionChanged += this.Games_CollectionChanged;

            this.Group.Update();
        }

        private void Games_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    this.GameViewModels.Clear();
                    break;

                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        foreach (Game item in e.NewItems)
                        {
                            this.GameViewModels.Add(new GameViewModel(item));
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (Game item in e.OldItems)
                        {
                            this.GameViewModels.Remove(this.GameViewModels.First(g => g.Game.Equals(item)));
                        }
                    }
                    break;
            }
        }

        private void ConvertGames(IList<Game> games)
        {
            this.GameViewModels.Clear();
            foreach (Game g in games)
            {
                this.GameViewModels.Add(new GameViewModel(g));
            }
        }
    }
}