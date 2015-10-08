using PostSharp.Patterns.Model;
using System;
using System.Collections.ObjectModel;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class Todo
    {
        #region properties
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime FinalDate { get; set; }
        public ObservableCollection<WorkingItem> WorkingItems { get; set; }
        #endregion

        #region ctor
        public Todo()
        {
            WorkingItems = new ObservableCollection<WorkingItem>();
        }
        #endregion
    }
}
