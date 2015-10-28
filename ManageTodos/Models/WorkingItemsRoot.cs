using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class WorkingItemsRoot
    {
        #region properties
        public ObservableCollection<WorkingItem> WorkingItems { get; private set; }
        #endregion

        #region ctor
        public WorkingItemsRoot()
        {
            WorkingItems = new ObservableCollection<WorkingItem>();
        }
        #endregion
    }
}
