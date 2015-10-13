using PostSharp.Patterns.Model;
using Prism.Commands;
using TimeManager.Shell.Services;

namespace TimeManager.Shell.ViewModels
{
    [NotifyPropertyChanged]
    public class MainWindowModel
    {
        #region ctor
        public MainWindowModel()
        {
            ApplicationMenuCommands.OpenCommand = new DelegateCommand(Open);
            ApplicationMenuCommands.SaveCommand = new DelegateCommand(Save);
        }
        #endregion

        #region methods
        private void Save()
        {

        }

        private void Open()
        {

        }
        #endregion
    }
}
