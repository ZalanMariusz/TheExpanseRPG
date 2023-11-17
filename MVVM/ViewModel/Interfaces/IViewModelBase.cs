using System.Collections.Generic;
using System.Windows.Controls;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel.Interfaces
{
    public interface IViewModelBase
    {
        public INavigationService NavigationService { get; }
        public List<IViewModelBase> InnerViewModels { get; }
        public List<Control>? OpenModals { get; }
        public IViewModelBase? GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase;
        public void AddInnerViewModel(IViewModelBase viewModel);
        public void SetCurrentInnerViewModel(IViewModelBase viewModel);
    }
}
