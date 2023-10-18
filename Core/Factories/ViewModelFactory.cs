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

        private readonly Func<Type, IViewModelBase> _factory;
        private void PopulateViewModelAssociationDictionary()
        {
            ViewModelAssociationDictionary = new Dictionary<Type, Type>
            {
                { typeof(WelcomeSplashWindow), typeof(WelcomeSplashViewModel) },
                { typeof(PlayerMainWindow), typeof(PlayerMainViewModel) },
                { typeof(GmMainWindow), typeof(GmMainViewModel) },
                { typeof(CharacterCreationWindow), typeof(CharacterCreationViewModel) },
                { typeof(TalentListWindow), typeof(TalentListViewModel) }
            };
        }

        public ViewModelFactory(Func<Type, IViewModelBase> factory)
        {
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
        }
    }
}
