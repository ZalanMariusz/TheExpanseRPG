using System.Collections.Generic;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel.Interfaces
{
    public interface IViewModelBase
    {
        public INavigationService NavigationService { get; }
        public IViewModelBase? GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase;
        public void AddInnerViewModel(IViewModelBase viewModel);
        public void SetCurrentInnerViewModel(IViewModelBase viewModel);
    }
}
