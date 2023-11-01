using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class ViewModelBase : PropertyNotifierObject, IViewModelBase
    {
        private IViewModelBase? _currentInnerViewModel;
        public IViewModelBase? CurrentInnerViewModel
        {
            get { return _currentInnerViewModel; }
            private set { _currentInnerViewModel = value; OnPropertyChanged(); }
        }

        private INavigationService? _navigationService;
        public INavigationService NavigationService
        {
            get { return _navigationService!; }
            protected set { _navigationService = value; OnPropertyChanged(); }
        }

        private List<IViewModelBase> _innerViewModels = new();
        public List<IViewModelBase> InnerViewModels
        {
            get { return _innerViewModels!; }
            protected set { _innerViewModels = value; OnPropertyChanged(); }
        }

        private List<Control>? _openModals;
        public List<Control>? OpenModals
        {
            get { return _openModals; }
            set { _openModals = value; OnPropertyChanged(); }
        }
        public IViewModelBase? GetInnerViewModel<TViewModelBase>() where TViewModelBase : IViewModelBase
        {
            return InnerViewModels.FirstOrDefault(x => x.GetType() == typeof(TViewModelBase));
        }
        public virtual void AddInnerViewModel(IViewModelBase viewModel)
        {
            if (InnerViewModels.FirstOrDefault(x => x.GetType() == viewModel.GetType()) == null)
            {
                InnerViewModels.Add(viewModel);
            }
        }
        public void SetCurrentInnerViewModel(IViewModelBase viewModel)
        {
            CurrentInnerViewModel = viewModel;
        }
    }
}
