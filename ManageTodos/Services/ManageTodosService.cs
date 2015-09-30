using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using TimeManager.ManageTodos.Models;
using TimeManager.ManageTodos.Properties;

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

        #region ctor
        public ManageTodosService()
        {
            todos = new ObservableCollection<Todo>();
            ReadTodos();
        }
        #endregion

        #region methods
        private void ReadTodos()
        {
            var document = XDocument.Parse(Resources.Todos);

            foreach (var todo in document.Descendants("Todo"))
            {
                todos.Add(new Todo()
                {
                    Title = todo.Element("Title").Value,
                    Description = todo.Element("Description").Value,
                    Priority = (Priority)Enum.Parse(typeof(Priority), todo.Element("Priority").Value),
                    FinalDate = DateTime.Parse(todo.Element("FinalDate").Value)
                });
            }
        }
        #endregion
    }
}
