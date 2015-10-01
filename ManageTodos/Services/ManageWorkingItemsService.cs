using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.Services
{
    public class ManageWorkingItemsService
    {
        #region fields
        private ObservableCollection<WorkingItem> workingItems;
        #endregion

        #region properties
        public ObservableCollection<WorkingItem> WorkingItems { get { return workingItems; } private set { workingItems = value; } }
        #endregion

        #region ctor
        public ManageWorkingItemsService()
        {
            workingItems = new ObservableCollection<WorkingItem>();
        }
        #endregion
    }
}
