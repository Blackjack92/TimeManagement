using PostSharp.Patterns.Model;
using Prism.Commands;
using System.Windows.Input;
using TimeManager.ManageTodos.Services;

namespace TimeManager.ManageTodos.ViewModels
{

    [NotifyPropertyChanged]
    public class TimeTrackingViewModel
    {
        #region properties
        public TimerService TimerService { get; private set; }

        public ICommand StartStopCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        #endregion

        #region ctor
        public TimeTrackingViewModel(TimerService timerService)
        {
            TimerService = timerService;

            StartStopCommand = new DelegateCommand(ToggleStartStop);
            ResetCommand = new DelegateCommand(Reset);
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
