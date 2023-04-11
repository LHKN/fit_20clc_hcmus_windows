// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyShop.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Forms;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyShop.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }
        
        //private void nvHomePage_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{
        //    FrameNavigationOptions navOptions = new FrameNavigationOptions();
        //    navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
        //    if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
        //    {
        //        navOptions.IsNavigationStackEnabled = false;
        //    }
        //    Type pageType = typeof(RootPage); //init

        //    var selectedItem = (NavigationViewItem)args.SelectedItem;

        //    if (selectedItem.Name == navItemDashboard.Name) { pageType = typeof(DashboardPage); }
        //    else if (selectedItem.Name == navItemBook.Name) { pageType = typeof(BookPage); }
        //    else if (selectedItem.Name == navItemAccount.Name) { pageType = typeof(AccountPage); }
        //    else if (selectedItem.Name == navItemOrderHistory.Name) { pageType = typeof(OrderHistoryPage); }
        //    else if (selectedItem.Name == navItemStatistic.Name) { pageType = typeof(StatisticPage); }
        //    else if (selectedItem.Name == navItemSetting.Name) { pageType = typeof(SettingPage); }

        //    if (homeViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(DashboardViewModel)
        //        && pageType == typeof(DashboardPage)) { homeViewModel.ChildPageNavigation.ViewModel = new DashboardViewModel(); }
        //    else if (homeViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(StatisticViewModel)
        //        && pageType == typeof(StatisticPage)) { homeViewModel.ChildPageNavigation.ViewModel = new StatisticViewModel(); }
        //    else if (homeViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(OrderHistoryViewModel)
        //        && pageType == typeof(OrderHistoryPage)) { homeViewModel.ChildPageNavigation.ViewModel = new OrderHistoryViewModel(); }
        //    else if (homeViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(BookViewModel)
        //        && pageType == typeof(BookPage)) { homeViewModel.ChildPageNavigation.ViewModel = new BookViewModel(); }
        //    else if (homeViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(AccountViewModel)
        //        && pageType == typeof(AccountPage)) { homeViewModel.ChildPageNavigation.ViewModel = new AccountViewModel(); }
        //    else if (homeViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(SettingViewModel)
        //        && pageType == typeof(SettingPage)) { homeViewModel.ChildPageNavigation.ViewModel = new SettingViewModel(); }

        //    _ = contentFrame.Navigate(pageType);
        //}
    }
}
