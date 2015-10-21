using Microsoft.Win32;
using System.Xml.Linq;
using TimeManager.ManageTodos.DataStoreObjects;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.Services
{
    public class DataStoreService
    {
        private readonly TodosRoot manageTodosService;
        private readonly WorkingItemsRoot manageWorkingItemsService;

        public DataStoreService(TodosRoot manageTodosService, WorkingItemsRoot manageWorkingItemsService)
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

                var todosElement = new XElement("Todos");
                var storeObject = new TodoDataStoreObject();

                foreach (var item in manageTodosService.Todos)
                {
                    todosElement.Add(storeObject.CreateXElement(item));
                }

                document.Add(todosElement);
                document.Save(file);
            }
        }
    }
}
