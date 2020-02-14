using BierPongTurnier.Common;
using BierPongTurnier.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Timers;
using System.Windows.Input;

namespace BierPongTurnier.Ui
{
    public enum State
    {
        Playing,
        Paused
    };

    public class GroupViewModel : BaseNotifyPropertyChanged
    {
        private Timer _timer;

        private int _gameDuration = 0;

        public int GameDuration
        {
            get => this._gameDuration;
            set
            {
                if (this._gameDuration != value)
                {
                    this._gameDuration = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int MaxGameDuration { get => Convert.ToInt32(TimeSpan.FromMinutes(Constants.GAMETIME_MINUTES).TotalSeconds); }

        private State _timerState = State.Paused;

        public State TimerState
        {
            get => this._timerState;
            set
            {
                if (this._timerState != value)
                {
                    this._timerState = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Group Group { get; }

        public string Name => this.Group.Name;

        public ObservableCollection<GameViewModel> GameViewModels { get; }

        public ICommand PlayPauseCommand { get; }

        public ICommand ResetCommand { get; }

        public GroupViewModel(Group group)
        {
            this.Group = group ?? throw new ArgumentNullException(nameof(group));
            this.GameViewModels = new ObservableCollection<GameViewModel>();

            this.PlayPauseCommand = new Command(() =>
            {
                if (_timerState == State.Paused)
                {
                    this.StartTimer();
                }
                else if (_timerState == State.Playing)
                {
                    this.StopTimer();
                }
            });
            this.ResetCommand = new Command(() => { this.GameDuration = 0; });

            this._timer = new Timer()
            {
                Interval = 1000,
                Enabled = true,
            };
            this._timer.Stop();
            this._timer.Elapsed += this.Timer_Elapsed;

            this.ConvertGames(group.Games);
            this.Group.Games.CollectionChanged += this.Games_CollectionChanged;

            this.Group.Update();
        }

        private void StartTimer()
        {
            this._timer.Start();
            TimerState = State.Playing;
        }

        private void StopTimer()
        {
            this._timer.Stop();
            TimerState = State.Paused;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.GameDuration >= this.MaxGameDuration)
            {
                this.GameDuration = this.MaxGameDuration;
                this.StopTimer();
                return;
            }

            this.GameDuration++;
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