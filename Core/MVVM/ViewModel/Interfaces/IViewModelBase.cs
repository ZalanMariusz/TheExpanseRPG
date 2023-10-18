using System.Collections.Generic;
using System.Windows.Controls;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel.Interfaces
{
    public interface IViewModelBase
    {
        public INavigationService NavigationService { get; }
        public List<IViewModelBase> InnerViewModels { get; }
        public List<Control> OpenModals { get; }
        public IViewModelBase? GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase;
        public void AddInnerViewModel(IViewModelBase viewModel);
        public void SetCurrentInnerViewModel(IViewModelBase viewModel);
    }
}
