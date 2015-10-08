using System;
using System.Xml.Linq;
using TimeManager.ManageTodos.Models;
using TimeManager.ManageTodos.Properties;

namespace TimeManager.ManageTodos.Services
{
    public class DataLoadService
    {
        #region fields
        private readonly ManageTodosService manageTodosService;
        private readonly ManageWorkingItemsService manageWorkingItemsService;
        #endregion

        #region ctor
        public DataLoadService(ManageTodosService manageTodosService, ManageWorkingItemsService manageWorkingItemsService)
        {
            this.manageTodosService = manageTodosService;
            this.manageWorkingItemsService = manageWorkingItemsService;
        }
        #endregion

        #region methods
        public void LoadData()
        {
            LoadWorkingItems();
            LoadTodos();
        }

        private void LoadTodos()
        {
            XDocument document = XDocument.Parse(Resources.Todos);
            foreach (XElement element in document.Descendants("Todo"))
            {
                Todo item = new Todo
                {
                    Id = Guid.Parse(element.Element("Id").Value),
                    Title = element.Element("Title").Value,
                    Description = element.Element("Description").Value,
                    Priority = (Priority)Enum.Parse(typeof(Priority), element.Element("Priority").Value),
                    FinalDate = DateTime.Parse(element.Element("FinalDate").Value)
                };
                manageTodosService.Todos.Add(item);
            }
        }

        private void LoadWorkingItems()
        {
            XDocument document = XDocument.Parse(Resources.WorkingItems);
            foreach (XElement element in document.Descendants("WorkingItem"))
            {
                WorkingItem item = new WorkingItem
                {
                    Id = Guid.Parse(element.Element("Id").Value),
                    Start = DateTime.Parse(element.Element("Start").Value),
                    End = DateTime.Parse(element.Element("End").Value),
                    Description = element.Element("Description").Value
                };
                manageWorkingItemsService.WorkingItems.Add(item);
            }
        }
        #endregion
    }
}

