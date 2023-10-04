using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using TheExpanseRPG.Core.Commands;
using TheExpanseRPG.Core.Enums;
using TheExpanseRPG.Core.MVVM.Model;
using TheExpanseRPG.Core.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Core.Services;
using TheExpanseRPG.Core.Services.Interfaces;

namespace TheExpanseRPG.Core.MVVM.ViewModel
{
    public class AttributeRollViewModel : ViewModelBase
    {
        private AttributeRollType _selectedAttributeRollType;
        public AttributeRollType SelectedAttributeRollType { get { return _selectedAttributeRollType; } set { _selectedAttributeRollType = value; ChangeRollType(); } }

        private void ChangeRollType()
        {
            OnPropertyChanged("SelectedAttributeRollType");
            switch (SelectedAttributeRollType)
            {
                case AttributeRollType.AllRandom:
                    NavigationService.NavigateToInnerView<AllRandomAttributeRollViewModel>(this);
                    break;
                case AttributeRollType.RollAndAssign:
                    NavigationService.NavigateToInnerView<AssignAttributeRollViewModel>(this);
                    break;
                case AttributeRollType.DistributePoints:
                    NavigationService.NavigateToInnerView<DistributeAttributesViewModel>(this);
                    break;
                default:
                    break;
            }
            
        }
 
        public AttributeRollViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SelectedAttributeRollType = AttributeRollType.AllRandom;
        }
       
    }
}
