using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    public class PageNavigation: ObservableObject
    {
        private ViewModelBase _viewModel;

        public PageNavigation(ViewModelBase viewModel)
        {
            ViewModel = viewModel;
        }

        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                ChangeViewModel(value);
            }
        }

        private void ChangeViewModel(ViewModelBase value)
        {
            SetProperty(ref _viewModel, value, nameof(ViewModel));
            _viewModel.ParentPageNavigation = this;
        }
    }
}
