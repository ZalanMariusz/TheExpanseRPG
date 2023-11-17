using System.Windows;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Factories.Interfaces
{
    public interface IViewModelFactory
    {
        public IViewModelBase GetWindowViewModel<TWindow>() where TWindow : Window;
        public IViewModelBase GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase;
    }
}
