using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;
using PostSharp.Patterns.Model;
using System;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class TodosRoot
    {
        #region properties
        public ObservableCollection<Todo> Todos { get; set; }
        #endregion

        #region ctor
        public TodosRoot()
        {
            Todos = new ObservableCollection<Todo>();
        }
        #endregion
    }
}
