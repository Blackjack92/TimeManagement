using Microsoft.Win32;
using System.Collections.Generic;
using System.Xml.Linq;
using TimeManager.Infrastructure.Data;
using TimeManager.ManageTodos.DataStoreObjects;
using TimeManager.ManageTodos.Models;

namespace TimeManager.ManageTodos.Services
{
    public class DataStoreService
    {
        #region fields
        private readonly TodosRoot todosRoot;
        private readonly IEnumerable<IStoreRoot> storeRoots;
        #endregion

        #region ctor
        public DataStoreService(TodosRoot todosRoot, IEnumerable<IStoreRoot> storeRoots)
        {
            this.todosRoot = todosRoot;
            this.storeRoots = storeRoots;
        }
        #endregion

        #region methods
        public void Save()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var file = dialog.FileName;

                XDocument document = new XDocument();
                
                // Store todos
                var storeObject = new TodosRootDataStoreObject();
                var todos = storeObject.CreateXElement(todosRoot);
                if (todos != null)
                {
                    document.Add(todos);
                    document.Save(file);
                }
            }
        }
        #endregion
    }
}
