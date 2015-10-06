using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure;
using TimeManager.ManageProjects.Views;

namespace TimeManager.ManageProjects
{
    public class ManageProjectsModule : IModule
    {
        #region fields
        private readonly IRegionManager regionManager;
        #endregion

        #region ctor
        public ManageProjectsModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        #endregion

        #region methods
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(MainView));
            regionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(RibbonView));
        }
        #endregion
    }
}
