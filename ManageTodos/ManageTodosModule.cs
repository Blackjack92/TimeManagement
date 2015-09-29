using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure;
using TimeManager.ManageTodos.Views;

namespace TimeManager.ManageTodos
{
    public class ManageTodosModule : IModule
    {
        IRegionManager regionManager;

        public ManageTodosModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(TodosMainView));
        }
    }
}
