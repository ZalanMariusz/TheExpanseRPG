using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel.Interfaces
{
    public interface IViewModelBase
    {
        public INavigationService NavigationService { get; }
    }
}
