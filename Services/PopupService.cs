using System.Linq;
using System.Windows;
using TheExpanseRPG.MVVM.ViewModel;
using TheExpanseRPG.Popups;

namespace TheExpanseRPG.Services
{
    public class PopupService
    {
        public MessageBoxResult ShowPopup(string message)
        {
            PopupViewModelBase popupVm = new(message);
            PopupBase popup = new()
            {
                DataContext = popupVm,
                //Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)
            };
            popup.ShowDialog();
            return popupVm.PopupResult;
        }
    }
}
