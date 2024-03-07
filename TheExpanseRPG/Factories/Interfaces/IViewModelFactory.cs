using System.Windows;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Factories.Interfaces
{
    public interface IViewModelFactory
    {
        public IViewModelBase GetWindowViewModel<TWindow>() where TWindow : Window;
        public IViewModelBase GetViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase;
        //public IViewModelBase GetPopupViewModel<TViewModelBase>() where TViewModelBase : PopupViewModelBase, IViewModelBase;
    }
}
