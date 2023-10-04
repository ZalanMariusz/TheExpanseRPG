using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Core.Factories.Interfaces;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    class NavigationService : PropertyNotifierObject, INavigationService
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IViewFactory _viewFactory;
        //private List<IViewModelBase> _innerViewModels;

        //private IViewModelBase? _currentViewModel;
        //public IViewModelBase CurrentViewModel
        //{
        //    get { return _currentViewModel!; }
        //    private set { _currentViewModel = value; OnPropertyChanged(); }
        //}

        public NavigationService(IViewModelFactory viewModelFactory, IViewFactory viewFactory)
        {
            _viewModelFactory = viewModelFactory;
            _viewFactory = viewFactory;
            //_innerViewModels = new List<IViewModelBase>();
        }
        public void NavigateToInnerView<TViewModelBase>(IViewModelBase owner) where TViewModelBase : IViewModelBase
        {
            
            IViewModelBase viewModel = owner.GetInnerViewModel<TViewModelBase>();
            IViewModelBase CurrentViewModel = viewModel != null ? viewModel : _viewModelFactory.GetInnerViewModel<TViewModelBase>();
            owner.AddInnerViewModel(CurrentViewModel);
            owner.SetCurrentInnerViewModel(CurrentViewModel);
        }
        public void NavigateToNewWindow<TWindow>(Window? sender = null, bool closeWindow = false) where TWindow : Window
        {
            //_innerViewModels.Clear();
            ShowWindow<TWindow>(sender,closeWindow);
        }
        public void NavigateToModal<TWindow>() where TWindow : Window
        {
            var window = _viewFactory.GetWindow<TWindow>();
            window.DataContext = _viewModelFactory.GetWindowViewModel<TWindow>();
            window.ShowDialog();
        }

        private void ShowWindow<TWindow>(Window? sender, bool closeWindow) where TWindow : Window
        {
            Window window = _viewFactory.GetWindow<TWindow>();
            window.DataContext = _viewModelFactory.GetWindowViewModel<TWindow>();
            window.Show();
            if (sender != null)
            {
                if (closeWindow)
                {
                    sender.Close();
                }
                else
                {
                    sender.Hide();
                }
            }
        }

        //private IViewModelBase GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase
        //{
        //    return _innerViewModels.FirstOrDefault(x => x.GetType() == typeof(TViewModelBase))!;
        //}
    }
}
