using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.Services
{
    public class ManageTodosService
    {
        #region fields
        private ObservableCollection<Todo> todos;
        #endregion

        #region properties
        public ObservableCollection<Todo> Todos { get { return todos; } private set { todos = value; } }
        #endregion
    }
}
