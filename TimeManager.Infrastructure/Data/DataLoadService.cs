using System.Collections.Generic;
using PostSharp.Patterns.Model;
using Microsoft.Win32;
using System.Xml.Linq;
using TimeManager.Infrastructure.Utils;

namespace TimeManager.Infrastructure.Data
{
    [NotifyPropertyChanged]
    public class DataLoadService
    {
        #region fields
        private readonly IEnumerable<IStoreRoot> rootsToLoad;
        #endregion

        #region ctor
        public DataLoadService(IEnumerable<IStoreRoot> rootsToLoad)
        {
            this.rootsToLoad = rootsToLoad;
        }
        #endregion

        #region methods
        public void Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var file = dialog.FileName;
                var document = XDocument.Load(file);

                // Store all store roots
                foreach (var root in rootsToLoad)
                {
                    foreach (XElement element in document.Descendants(root.GetType().Name))
                    {
                        var storeClass = DataStoreHelper.FindFirstStoreClass(root.GetType());
                        var createdObject = DataStoreHelper.CreateObject(storeClass, new object[] { element });

                        StaticReflection.SetValuesFromObject(root, createdObject);
                    }
                }
            }
        }
        #endregion
    }
}