using System.Windows;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Core.Factories.Interfaces
{
    public interface IViewModelFactory
    {
        public IViewModelBase GetWindowViewModel<TWindow>() where TWindow : Window;
        public IViewModelBase GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase;
    }
}
