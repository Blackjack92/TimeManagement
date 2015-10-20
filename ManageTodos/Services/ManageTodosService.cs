using System.Collections.ObjectModel;
using TimeManager.ManageTodos.Models;
using PostSharp.Patterns.Model;
using TimeManager.Infrastructure.Interfaces;
using System;
using System.Xml.Linq;

namespace TimeManager.ManageTodos.Services
{
    [NotifyPropertyChanged]
    public class ManageTodosService : IXmlable
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

        #region methods
        public XElement TransformToXml()
        {
            return null;
        }
        #endregion
    }
}
