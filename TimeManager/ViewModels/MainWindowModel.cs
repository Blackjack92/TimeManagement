using PostSharp.Patterns.Model;
using Prism.Commands;
using Prism.Events;
using TimeManager.Infrastructure;
using TimeManager.Infrastructure.Events;
using TimeManager.Shell.Services;

namespace TimeManager.Shell.ViewModels
{
    [NotifyPropertyChanged]
    public class MainWindowModel
    {
        private readonly IEventAggregator eventAggregator;

        #region ctor
        public MainWindowModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            ApplicationMenuCommands.OpenCommand = new DelegateCommand(Open);
            ApplicationMenuCommands.SaveCommand = new DelegateCommand(Save);
        }
        #endregion

        #region methods
        private void Open()
        {
            eventAggregator.GetEvent<StringEvent>().Publish(ApplicationMenuNames.Open);
        }

        private void Save()
        {
            eventAggregator.GetEvent<StringEvent>().Publish(ApplicationMenuNames.Save);
        }
        #endregion
    }
}
