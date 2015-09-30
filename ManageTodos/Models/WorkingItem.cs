using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace TimeManager.ManageTodos.Models
{
    public class WorkingItem : BindableBase
    {
        #region fields
        private Todo todo;
        private ObservableCollection<WorkingTime> workingTimes;
        #endregion

        #region properties
        public Todo Todo { get { return todo; } set { SetProperty(ref todo, value); } }
        public ObservableCollection<WorkingTime> WorkingTimes { get { return workingTimes; } private set { SetProperty(ref workingTimes, value); } }
        #endregion

        #region ctor
        public WorkingItem()
        {
            workingTimes = new ObservableCollection<WorkingTime>();
        }
        #endregion
    }
}
