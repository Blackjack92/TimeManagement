using Prism.Modularity;
using Prism.Regions;
using TimeManager.Infrastructure.Names;
using TimeManager.Navigation.Views;

namespace TimeManager.Navigation
{
    public class NavigationModule : IModule
    {
        #region fields
        private readonly IRegionManager regionManager;
        #endregion

        #region ctor
        public NavigationModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        #endregion

        #region methods
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationView));
        }
        #endregion
    }
}
