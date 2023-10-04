using System.Windows;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Core.Services.Interfaces
{
    public interface INavigationService
    {
        public void NavigateToNewWindow<TWindow>(Window? Sender = null,bool closeWindow = false) where TWindow : Window;
        public void NavigateToInnerView<TViewModelBase>(IViewModelBase owner) where TViewModelBase : IViewModelBase;
        public void NavigateToModal<TWindow>() where TWindow : Window;
        //public IViewModelBase CurrentViewModel { get; }
    }
}
