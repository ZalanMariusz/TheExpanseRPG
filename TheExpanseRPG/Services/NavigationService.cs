using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.Factories.Interfaces;
using TheExpanseRPG.Core.Model;
using TheExpanseRPG.Factories.Interfaces;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.MVVM.ViewModel;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.Services
{
    public class NavigationService : INavigationService
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
            IViewModelBase CurrentViewModel = owner.GetInnerViewModel<TViewModelBase>() ?? _viewModelFactory.GetViewModel<TViewModelBase>();
            owner.AddInnerViewModel(CurrentViewModel);
            owner.SetCurrentInnerViewModel(CurrentViewModel);
        }
        public void NavigateToNewWindow<TWindow>(Window? sender = null, bool closeWindow = false) where TWindow : Window
        {
            ShowWindow<TWindow>(sender, closeWindow);
        }
        public void NavigateToCharacterSheet(IViewModelBase sender, object character)
        {
            var window = _viewFactory.GetWindow<CharacterSheetWindow>();
            window.DataContext = _viewModelFactory.GetWindowViewModel<CharacterSheetWindow>();
            ((CharacterSheetViewModel)window.DataContext).Character = (ExpanseCharacter)character!;
            sender?.OpenModals?.Add(window);
            window.ShowDialog();
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
                if (modal.IsLoaded)
                {
                    modal.Visibility = Visibility.Visible;
                    modal.Activate();
                }
                else
                {
                    if (isDialog)
                    {
                        modal.ShowDialog();
                    }
                    else
                    {
                        modal.Show();
                    }
                }

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
            App.IsNavigating = true;
            if (closeWindow)
            {
                sender?.Close();
            }
            else
            {
                sender?.Hide();
            }
            App.IsNavigating = false;
        }
    }
}
