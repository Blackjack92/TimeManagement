using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TimeManager.ManageTodos.Data;

namespace TimeManager.ManageTodos.Services
{
    public class DataStoreService
    {
        private readonly ManageTodosService manageTodosService;
        private readonly ManageWorkingItemsService manageWorkingItemsService;

        public DataStoreService(ManageTodosService manageTodosService, ManageWorkingItemsService manageWorkingItemsService)
        {
            this.manageTodosService = manageTodosService;
            this.manageWorkingItemsService = manageWorkingItemsService;
        }

        public void Save()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var file = dialog.FileName;

                XDocument document = new XDocument();

                var element = new TodoDataStoreObject();
                var todo = manageTodosService.Todos.First();
                var ele = element.CreateXElement(todo);

                document.Add(new XElement("root",
                    new XElement("children",
                        new object[]{ new XElement("child1"),
                        new XElement("child2"),
                        new XElement("child3") })));

                document.Save(file);
            }
        }
    }
}
