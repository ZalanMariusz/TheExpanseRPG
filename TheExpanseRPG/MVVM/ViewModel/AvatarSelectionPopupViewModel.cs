using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using TheExpanseRPG.Commands;

namespace TheExpanseRPG.MVVM.ViewModel;

public class AvatarSelectionPopupViewModel : ViewModelBase
{
    public ObservableCollection<string> AvatarList { get; set; }
    private string _selectedAvatar;
    public string SelectedAvatar
    {
        get { return _selectedAvatar; }
        set { _selectedAvatar = value; OnPropertyChanged(); }
    }
    public RelayCommand SelectAvatarCommand { get; set; }
    public AvatarSelectionPopupViewModel(string currentAvatar)
    {
        SelectedAvatar = currentAvatar;
        AvatarList = new(Directory.GetFiles($".{WPFStringResources.AvatarFolderPath}"));
        for (int i = 0; i < AvatarList.Count; i++)
        {
            AvatarList[i] = AvatarList[i].Replace(".\\", "\\");
        }
        SelectAvatarCommand = new(o => true, SelectAvatar);
    }
    private void SelectAvatar(object param)
    {
        object sender = ((object[])param)[0];
        SelectedAvatar = ((object[])param)[1].ToString()!;


        (sender as Window).Close();
    }

}
