using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;
using PostSharp.Patterns.Model;
using System;

namespace TimeManager.ManageTodos.Services
{
    [NotifyPropertyChanged]
    public class ManageTodosService
    {
        #region properties
        public ObservableCollection<Todo> Todos { get; set; }
        #endregion

        #region ctor
        public ManageTodosService()
        {
            Todos = new ObservableCollection<Todo>();
        }
        #endregion
    }
}
