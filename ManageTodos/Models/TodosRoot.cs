using System.Collections.ObjectModel;
using PostSharp.Patterns.Model;
using TimeManager.Infrastructure.Data;

namespace TimeManager.ManageTodos.Models
{
    [NotifyPropertyChanged]
    public class TodosRoot : IStoreRoot
    {
        #region properties
        public ObservableCollection<Todo> Todos { get; private set; }
        #endregion

        #region ctor
        public TodosRoot()
        {
            Todos = new ObservableCollection<Todo>();
        }
        #endregion
    }
}
