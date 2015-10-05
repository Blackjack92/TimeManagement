using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.Services
{
    [NotifyPropertyChanged]
    public class ManageWorkingItemsService
    {
        #region properties
        public ObservableCollection<WorkingItem> WorkingItems { get; set; }
        #endregion

        #region ctor
        public ManageWorkingItemsService()
        {
            WorkingItems = new ObservableCollection<WorkingItem>();
        }
        #endregion
    }
}
