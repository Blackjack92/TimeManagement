using PostSharp;
using PostSharp.Patterns.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using TimeManager.Infrastructure;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class Todo
    {
        public static class TodoProperties
        {
            public static string Done = StaticReflection.GetMemberName<Todo>(todo => todo.Done);
        }

        #region properties
        public Guid Id { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime FinalDate { get; set; }
        public ObservableCollection<WorkingItem> WorkingItems { get; set; }
        public TimeSpan SpentTime { get; private set; }
        #endregion

        #region ctor
        public Todo()
        {
            WorkingItems = new ObservableCollection<WorkingItem>();
            WorkingItems.CollectionChanged += OnWorkingItemsCollectionChanged;
        }

        private void UpdateSpentTime()
        {
            SpentTime = WorkingItems.Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal.Add(t.SpentTime));
        }

        private void OnWorkingItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSpentTime();

            // Update on working item changes
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(var item in e.NewItems.OfType<WorkingItem>())
                {
                    Post.Cast<WorkingItem, INotifyPropertyChanged>(item).PropertyChanged += OnWorkingItemsCollectionItemChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems.OfType<WorkingItem>())
                {
                    Post.Cast<WorkingItem, INotifyPropertyChanged>(item).PropertyChanged -= OnWorkingItemsCollectionItemChanged;
                }
            }
        }

        private void OnWorkingItemsCollectionItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSpentTime();
        }
        #endregion
    }
}
