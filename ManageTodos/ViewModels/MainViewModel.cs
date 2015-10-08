using Prism.Commands;
using TimeManager.ManageTodos.Services;
using System;
using TimeManager.ManageTodos.Models;
using PostSharp.Patterns.Model;
using PostSharp;
using System.ComponentModel;
using TimeManager.Infrastructure;
using System.Windows.Data;

namespace TimeManager.ManageTodos.ViewModels
{
    [NotifyPropertyChanged]
    public class MainViewModel
    {
        public static class MainViewModelProperties
        {
            public static string SelectedTodo = StaticReflection.GetMemberName<MainViewModel>(model => model.SelectedTodo);
            public static string SelectedWorkingItem = StaticReflection.GetMemberName<MainViewModel>(model => model.SelectedWorkingItem);
        }

        #region properties
        public ManageTodosService ManageTodosService { get; private set; }
        public ManageWorkingItemsService ManageWorkingItemsService { get; private set; }
        public Todo SelectedTodo { get; set; }
        public WorkingItem SelectedWorkingItem { get; set; }
        public ListCollectionView GroupedTodos { get; private set; }
        #endregion

        #region ctor
        public MainViewModel(ManageTodosService manageTodosService, ManageWorkingItemsService manageWorkingItemsService, DataLoadService dataLoadService)
        {
            ManageTodosService = manageTodosService;
            ManageWorkingItemsService = manageWorkingItemsService;

            dataLoadService.LoadData();
            RibbonCommands.AddTodoCommand = new DelegateCommand(AddTodo);
            RibbonCommands.RemoveTodoCommand = new DelegateCommand(RemoveTodo, CanRemoveTodo);
            RibbonCommands.AddWorkingItemCommand = new DelegateCommand(AddWorkingItem, CanAddWorkingItem);
            RibbonCommands.RemoveWorkingItemCommand = new DelegateCommand(RemoveWorkingItem, CanRemoveWorkingItem);

            Post.Cast<MainViewModel, INotifyPropertyChanged>(this).PropertyChanged += OnPropertyChanged;

            GroupedTodos = new ListCollectionView(manageTodosService.Todos);
            GroupedTodos.GroupDescriptions.Add(new PropertyGroupDescription(Todo.TodoProperties.Done));
            GroupedTodos.SortDescriptions.Add(new SortDescription(Todo.TodoProperties.Done, ListSortDirection.Ascending));
        }
        #endregion

        #region methods
        private void AddWorkingItem()
        {
            var item = new WorkingItem();
            SelectedTodo.WorkingItems.Add(item);
            ManageWorkingItemsService.WorkingItems.Add(item);
        }

        private bool CanAddWorkingItem()
        {
            return SelectedTodo != null;
        }

        private void RemoveWorkingItem()
        {
            SelectedTodo.WorkingItems.Remove(SelectedWorkingItem);
            ManageWorkingItemsService.WorkingItems.Remove(SelectedWorkingItem);
            SelectedWorkingItem = null;
        }

        private bool CanRemoveWorkingItem()
        {
            return SelectedTodo != null && SelectedWorkingItem != null;
        }

        private void AddTodo()
        {
            ManageTodosService.Todos.Add(new Todo() { Id = Guid.NewGuid(), FinalDate = DateTime.Now, Priority = Priority.High, Title = "Title", Description = "Description" });
        }

        private void RemoveTodo()
        {
            ManageTodosService.Todos.Remove(SelectedTodo);
        }

        private bool CanRemoveTodo()
        {
            return SelectedTodo != null;
        }
        
        private void UpdateCommands()
        {
            RibbonCommands.RemoveTodoCommand.RaiseCanExecuteChanged();
            RibbonCommands.AddWorkingItemCommand.RaiseCanExecuteChanged();
            RibbonCommands.RemoveWorkingItemCommand.RaiseCanExecuteChanged();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == MainViewModelProperties.SelectedTodo)
            {
                SelectedWorkingItem = null;
                UpdateCommands();
            }

            if(e.PropertyName == MainViewModelProperties.SelectedWorkingItem)
            {
                UpdateCommands();
            }
        }
        #endregion
    }
}
