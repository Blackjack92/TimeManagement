using System;
using System.ComponentModel.Composition;
using System.Xml.Linq;
using TimeManager.ManageTodos.Models;
using TimeManager.ManageTodos.Properties;

namespace TimeManager.ManageTodos.Services
{
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public class DataLoadService
    {
        private readonly ManageTodosService manageTodosService;
        private readonly ManageWorkingItemsService manageWorkingItemsService;

        [ImportingConstructor]
        public DataLoadService(ManageTodosService manageTodosService, ManageWorkingItemsService manageWorkingItemsService)
        {
            this.manageTodosService = manageTodosService;
            this.manageWorkingItemsService = manageWorkingItemsService;
        }

        public void LoadData()
        {
            this.LoadWorkingItems();
            this.LoadTodos();
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
                this.manageTodosService.Todos.Add(item);
            }
        }

        private void LoadWorkingItems()
        {
            XDocument document = XDocument.Parse(Resources.WorkingItems);
            foreach (XElement element in document.Descendants("WorkingItem"))
            {
                WorkingItem item = new WorkingItem
                {
                    Id = Guid.Parse(element.Element("Id").Value)
                };
                foreach (XElement element2 in element.Element("WorkingTimes").Elements("WorkingTime"))
                {
                    WorkingTime time = new WorkingTime
                    {
                        Start = DateTime.Parse(element2.Attribute("Start").Value),
                        End = DateTime.Parse(element2.Attribute("End").Value)
                    };
                    item.WorkingTimes.Add(time);
                }
                this.manageWorkingItemsService.WorkingItems.Add(item);
            }
        }
    }
}

