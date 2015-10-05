using System;
using System.Collections.ObjectModel;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.Models
{

    [NotifyPropertyChanged]
    public class WorkingItem
    {
        #region properties
        public Guid Id { get; set; }
        public ObservableCollection<WorkingTime> WorkingTimes { get; set; }
        #endregion

        #region ctor
        public WorkingItem()
        {
            WorkingTimes = new ObservableCollection<WorkingTime>();
        }
        #endregion
    }
}
