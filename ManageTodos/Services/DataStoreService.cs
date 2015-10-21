using Microsoft.Win32;
using System.Collections.Generic;
using System.Xml.Linq;
using TimeManager.Infrastructure.Data;

namespace TimeManager.ManageTodos.Services
{
    public class DataStoreService
    {
        #region fields
        private const string DocumentRootElementName = "TimeManager";
        private readonly IEnumerable<IStoreRoot> rootsToStore;
        #endregion

        #region ctor
        public DataStoreService(IEnumerable<IStoreRoot> rootsToStore)
        {
            this.rootsToStore = rootsToStore;
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
                var documentRootElement = new XElement(DocumentRootElementName);

                // Store all store roots
                foreach (var root in rootsToStore)
                {
                    var storeClass = DataStoreHelper.FindFirstStoreClass(root.GetType());
                    var xElement = DataStoreHelper.CreateXElement(storeClass, root);
                    documentRootElement.Add(xElement);
                }

                document.Add(documentRootElement);
                document.Save(file);
            }
        }
        #endregion
    }
}
