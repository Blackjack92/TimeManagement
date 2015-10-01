using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace TimeManager.ManageTodos.Models
{
    public class WorkingItem : BindableBase
    {
        private Guid id;
        private ObservableCollection<WorkingTime> workingTimes = new ObservableCollection<WorkingTime>();

        public Guid Id { get { return id; } set { SetProperty(ref id, value); } }
        public ObservableCollection<WorkingTime> WorkingTimes { get { return workingTimes; } private set { SetProperty(ref workingTimes, value); } }
    }
}
