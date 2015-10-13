using PostSharp.Patterns.Model;
using Prism.Commands;
using System;
using System.Windows.Input;
using TimeManager.ManageTodos.Models;
using TimeManager.ManageTodos.Services;

namespace TimeManager.ManageTodos.ViewModels
{

    [NotifyPropertyChanged]
    public class TimeTrackingViewModel
    {
        #region fields
        private readonly ISelectedTodoProvider selectedTodoProvider;
        private readonly ManageWorkingItemsService manageWorkingItemsService;
        #endregion

        #region properties
        public TimerService TimerService { get; private set; }
        public string Description { get; set; }

        public ICommand StartStopCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        #endregion

        #region ctor
        public TimeTrackingViewModel(TimerService timerService, ISelectedTodoProvider selectedTodoProvider, ManageWorkingItemsService manageWorkingItemsService)
        {
            TimerService = timerService;
            this.selectedTodoProvider = selectedTodoProvider;
            this.manageWorkingItemsService = manageWorkingItemsService;

            StartStopCommand = new DelegateCommand(ToggleStartStop);
            ResetCommand = new DelegateCommand(Reset);

            TimerService.TimeTracked += OnTimeTracked;
        }

        private void OnTimeTracked(object sender, EventArgs.TimeTrackedEventArgs e)
        {
            if (selectedTodoProvider.SelectedTodo == null) { return; }

            // Add new working item to the manage working item service
            var newWorkingItem = new WorkingItem() { Id = Guid.NewGuid(), Start = e.StartTime, End = e.EndTime, Description = Description };
            manageWorkingItemsService.WorkingItems.Add(newWorkingItem);
            selectedTodoProvider.SelectedTodo.WorkingItems.Add(newWorkingItem);

        }
        #endregion

        #region methods
        private void ToggleStartStop()
        {
            TimerService.ToggleStartStop();
        }

        private void Reset()
        {
            TimerService.Reset();
        }
        #endregion
    }
}
