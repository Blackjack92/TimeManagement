using Microsoft.Win32;
using System;
using System.Windows;
using System.Xml.Linq;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.DataStoreObjects;
using TimeManager.ManageTodos.Models;
using TimeManager.ManageTodos.Properties;

namespace TimeManager.ManageTodos.Services
{
    public class DataLoadService
    {
        #region fields
        private readonly TodosRoot manageTodosService;
        private readonly WorkingItemsRoot manageWorkingItemsService;
        #endregion

        #region ctor
        public DataLoadService(TodosRoot manageTodosService, WorkingItemsRoot manageWorkingItemsService)
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
                    FinalDate = DateTime.Parse(element.Element("FinalDate").Value),
                    Done = bool.Parse(element.Element("Done").Value)
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

        public void Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var file = dialog.FileName;
                var document = XDocument.Load(file);
                foreach (XElement element in document.Descendants("TodosRoot"))
                {
                    var storeObject = new TodosRootDataStoreObject();
                    var todosRoot = storeObject.CreateObject(element);
                }
            }
        }
        #endregion
    }
}

