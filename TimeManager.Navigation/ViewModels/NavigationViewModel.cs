﻿using Prism.Commands;
using Prism.Regions;
using System;
using System.Windows.Input;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Diagnostics;
using TimeManager.Infrastructure.Names;

namespace TimeManager.Navigation.ViewModels
{
    [NotifyPropertyChanged]
    public class NavigationViewModel
    {
        #region fields
        private readonly IRegionManager regionManager;
        #endregion

        #region properties
        public ICommand NavigationCommand { get; private set; }
        #endregion

        #region ctor
        public NavigationViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            NavigationCommand = new DelegateCommand<string>(NavigateTo);
        }
        #endregion

        #region methods
        [Log]
        private void NavigateTo(string view)
        {
            regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(view + ".MainView", UriKind.Relative));
            regionManager.RequestNavigate(RegionNames.RibbonRegion, new Uri(view + ".RibbonView", UriKind.Relative));
        }
        #endregion
    }
}
