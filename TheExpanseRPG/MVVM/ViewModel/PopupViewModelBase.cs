using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TheExpanseRPG.Commands;
using TheExpanseRPG.MVVM.ViewModel.Interfaces;
using TheExpanseRPG.Services.Interfaces;

namespace TheExpanseRPG.MVVM.ViewModel
{
    public class PopupViewModelBase
    {
        public MessageBoxResult PopupResult { get; set; }
        public string Message { get; set; }
        public RelayCommand OkCommand { get; set; }
        public virtual RelayCommand CancelCommand { get; set; }

        public PopupViewModelBase(string message)
        {
            Message = message;
            OkCommand = new RelayCommand(o => true, Ok);
            CancelCommand = new RelayCommand(o => true, Cancel);
        }
        private void Ok(object sender)
        {
            PopupResult = MessageBoxResult.OK;
            (sender as Window).Close();
        }
        private void Cancel(object sender)
        {
            PopupResult = MessageBoxResult.Cancel;
            (sender as Window).Close();
        }
    }
}
