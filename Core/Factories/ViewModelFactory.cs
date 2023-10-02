using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TheExpanseRPG.Core.Factories.Interfaces;
using TheExpanseRPG.Core.MVVM.View;
using TheExpanseRPG.Core.MVVM.ViewModel;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Core.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private Dictionary<Type, Type>? ViewModelAssociationDictionary;

        //private readonly Func<IEnumerable<Type>> _viewModelStore;
        private Func<Type, IViewModelBase> _factory;
        private void PopulateViewModelAssociationDictionary()
        {
            ViewModelAssociationDictionary = new Dictionary<Type, Type>
            {
                { typeof(WelcomeSplashWindow), typeof(WelcomeSplashViewModel) },
                { typeof(PlayerMainWindow), typeof(PlayerMainViewModel) },
                { typeof(GmMainWindow), typeof(GmMainViewModel) },
                { typeof(CharacterCreationWindow), typeof(CharacterCreationViewModel) }
                //{ typeof(OriginSelectView), typeof(OriginSelectViewModel) }
            };
        }

        public ViewModelFactory(/*Func<IEnumerable<Type>> viewModelStore,*/Func<Type, IViewModelBase> factory)
        {
            //_viewModelStore = viewModelStore;
            _factory = factory;
            PopulateViewModelAssociationDictionary();
        }

        public IViewModelBase GetWindowViewModel<TWindow>() where TWindow : Window
        {
            var viewmodel = ViewModelAssociationDictionary!.Single(x => typeof(TWindow) == x.Key).Value;
            return _factory(viewmodel);
        }
        public IViewModelBase GetInnerViewModel<TViewModelType>() where TViewModelType : IViewModelBase
        {
            return _factory(typeof(TViewModelType));
               // _viewModelStore().Where(x => x.GetType() == typeof(TViewModelType)).FirstOrDefault()!;
        }
    }
}
