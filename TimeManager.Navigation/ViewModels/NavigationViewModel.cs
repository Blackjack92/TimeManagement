using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;
using TimeManager.Infrastructure;

namespace TimeManager.Navigation.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public ICommand NavigationCommand { get; private set; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            NavigationCommand = new DelegateCommand<string>(NavigateTo);
        }

        private void NavigateTo(string view)
        {
            regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(view, UriKind.Relative));
        }

    }
}
