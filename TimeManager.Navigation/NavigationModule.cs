using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Infrastructure;
using TimeManager.Navigation.Views;

namespace TimeManager.Navigation
{
    public class NavigationModule : IModule
    {
        IRegionManager regionManager;

        public NavigationModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationView));
        }
    }
}
