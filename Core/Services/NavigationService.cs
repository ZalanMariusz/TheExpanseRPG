using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Factories.Interfaces;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.Services
{
    class NavigationService : INavigationService
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly IViewFactory _viewFactory;

        public NavigationService(IViewModelFactory viewModelFactory, IViewFactory viewFactory)
        {
            _viewModelFactory = viewModelFactory;
            _viewFactory = viewFactory;
        }
        public void NavigateToInnerView<TViewModelBase>(IViewModelBase owner) where TViewModelBase : IViewModelBase
        {
            IViewModelBase viewModel = owner.GetInnerViewModel<TViewModelBase>()!;
            IViewModelBase CurrentViewModel = viewModel ?? _viewModelFactory.GetInnerViewModel<TViewModelBase>();
            owner.AddInnerViewModel(CurrentViewModel);
            owner.SetCurrentInnerViewModel(CurrentViewModel);
        }
        public void NavigateToNewWindow<TWindow>(Window? sender = null, bool closeWindow = false) where TWindow : Window
        {
            ShowWindow<TWindow>(sender, closeWindow);
        }
        public void NavigateToModal<TWindow>(IViewModelBase sender, bool isDialog = true) where TWindow : Window
        {
            Window? modal = (Window?)sender.OpenModals?.FirstOrDefault(x => x.GetType() == typeof(TWindow));
            if (modal == null)
            {
                var window = _viewFactory.GetWindow<TWindow>();
                window.DataContext = _viewModelFactory.GetWindowViewModel<TWindow>();
                sender?.OpenModals?.Add(window);
                if (isDialog)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            }
            else
            {
                modal.Visibility = Visibility.Visible;
                modal.Activate();
            }
        }
        private void ShowWindow<TWindow>(Window? sender, bool closeWindow) where TWindow : Window
        {
            Window window = _viewFactory.GetWindow<TWindow>();
            window.DataContext = _viewModelFactory.GetWindowViewModel<TWindow>();
            window.Show();

            CleanPreviousModals(sender);
            CloseSender(sender, closeWindow);
        }

        private static void CleanPreviousModals(Window? sender)
        {
            List<Control>? openModals = (sender?.DataContext as IViewModelBase)?.OpenModals;
            if (openModals != null)
            {
                foreach (Control openModal in openModals)
                {
                    if (openModal is Window window)
                    {
                        window.Close();
                    }
                }
                openModals.Clear();
            }
        }
        private static void CloseSender(Window? sender, bool closeWindow)
        {
            if (closeWindow)
            {
                sender?.Close();
            }
            else
            {
                sender?.Hide();
            }
        }


    }
}
