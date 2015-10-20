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
