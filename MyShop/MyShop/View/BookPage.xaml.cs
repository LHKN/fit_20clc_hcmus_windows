// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyShop.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyShop.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookPage : Page
    {
        private BookViewModel bookViewModel { get; set; }
        public BookPage()
        {
            this.InitializeComponent();
            bookViewModel = new BookViewModel();
        }


        private void nvBookPage_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Auto)
            {
                navOptions.IsNavigationStackEnabled = false;
            }
            Type pageType = typeof(BooksPage); //init
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem.Name == navItemBooks.Name)
            {
                pageType = typeof(BooksPage);
            }
            else if (selectedItem.Name == navItemBookTypes.Name)
            {
                pageType = typeof(BookTypePage);
            }


            if (bookViewModel.ChildPageNavigation.ViewModel.GetType() != typeof(DashboardViewModel))
            {
                if (pageType == typeof(BooksPage))
                {
                    bookViewModel.ChildPageNavigation.ViewModel = new BooksViewModel();
                }
                else if (pageType == typeof(BookTypePage))
                {
                    bookViewModel.ChildPageNavigation.ViewModel = new BookTypeViewModel();
                }

                _ = contentFrame.Navigate(pageType);
            }
        }
    }
}
