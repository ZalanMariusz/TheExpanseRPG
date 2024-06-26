﻿using System;
using System.Collections.Generic;
using System.Windows;
using TheExpanseRPG.Factories.Interfaces;
using TheExpanseRPG.MVVM.View;
using TheExpanseRPG.MVVM.ViewModel;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;

namespace TheExpanseRPG.Factories;

public class ViewModelFactory : IViewModelFactory
{
    private Dictionary<Type, Type> ViewModelAssociationDictionary { get; set; } = new();

    private readonly Func<Type, IViewModelBase> _factory;
    private void PopulateViewModelAssociationDictionary()
    {
        ViewModelAssociationDictionary = new Dictionary<Type, Type>
        {
            { typeof(WelcomeSplashWindow), typeof(WelcomeSplashViewModel) },
            { typeof(PlayerMainWindow), typeof(PlayerMainViewModel) },
            { typeof(GmMainWindow), typeof(GmMainViewModel) },
            { typeof(CharacterCreationWindow), typeof(CharacterCreationViewModel) },
            { typeof(TalentListWindow), typeof(TalentListViewModel) },
            { typeof(FocusListWindow), typeof(FocusListViewModel) },
            { typeof(TalentInfoWindow), typeof(TalentInfoViewModel) },
            { typeof(CharacterSheetWindow), typeof(CharacterSheetViewModel) }
        };
    }

    public ViewModelFactory(Func<Type, IViewModelBase> factory)
    {
        _factory = factory;
        PopulateViewModelAssociationDictionary();
    }

    public IViewModelBase GetWindowViewModel<TWindow>() where TWindow : Window
    {
        Type viewmodel = ViewModelAssociationDictionary[typeof(TWindow)];
        return _factory(viewmodel);
    }
    public IViewModelBase GetViewModel<TViewModelType>() where TViewModelType : IViewModelBase
    {
        return _factory(typeof(TViewModelType));
    }
}
