using Prism.Commands;
using System.Windows.Input;
using TimeManager.ManageTodos.Services;
using System;
using TimeManager.ManageTodos.Models;
using System.Linq;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        #region properties
        public ManageTodosService ManageTodosService { get; private set; }
        public ManageWorkingItemsService ManageWorkingItemsService { get; private set; }
        #endregion

        #region ctor
        public MainViewModel(ManageTodosService manageTodosService, ManageWorkingItemsService manageWorkingItemsService, DataLoadService dataLoadService)
        {
            ManageTodosService = manageTodosService;
            ManageWorkingItemsService = manageWorkingItemsService;

            dataLoadService.LoadData();
            RibbonCommands.ChangeCommand = new DelegateCommand(Change);
        }

        private void Change()
        {
            ManageTodosService.Todos.Add(new Todo() { Id = Guid.NewGuid(), FinalDate = DateTime.Now, Priority = Priority.High, Title = "Title", Description = "Description" });
            var todo = ManageTodosService.Todos.ElementAt(0);
            todo.Title = "Juhuuuuuuuuu";
        }
        #endregion
    }
}
