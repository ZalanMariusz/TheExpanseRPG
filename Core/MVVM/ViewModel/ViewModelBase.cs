using System.Windows;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class ViewModelBase : PropertyNotifierObject, IViewModelBase
    {
        private INavigationService? _navigationService;
        public INavigationService NavigationService
        {
            get { return _navigationService!; }
            protected set { _navigationService = value; OnPropertyChanged(); }
        }
    }
}
