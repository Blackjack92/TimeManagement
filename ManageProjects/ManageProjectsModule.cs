using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure;
using TimeManager.ManageProjects.Views;

namespace TimeManager.ManageProjects
{
    public class ManageProjectsModule : IModule
    {
        IRegionManager regionManager;

        public ManageProjectsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
        }
    }
}
