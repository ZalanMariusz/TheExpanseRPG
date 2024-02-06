using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TheExpanseRPG.MVVM.ViewModel;

namespace TheExpanseRPG.Popups
{
    /// <summary>
    /// Interaction logic for AvatarSelectionPopup.xaml
    /// </summary>
    public partial class AvatarSelectionPopup : Window
    {

        public AvatarSelectionPopup()
        {
            Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (Owner != null)
            {
                Width = Owner.Width;
                Height = Owner.Height;
            }
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<Task> ImageGenerators = new();

            foreach (string imagePath in ((AvatarSelectionPopupViewModel)DataContext).AvatarList)
            {
                ImageGenerators.Add(GenerateAvatarImage(imagePath));
            }

            await Task.WhenAll(ImageGenerators);
            //await Task.Run(() =>
            //    Parallel.ForEach(((AvatarSelectionPopupViewModel)DataContext).AvatarList, async imagePath => await GenerateAvatarImage(imagePath)));

            labelLoading.Visibility = Visibility.Hidden;
        }
        private async Task GenerateAvatarImage(string avatarPath)
        {
            await Application.Current.Dispatcher.BeginInvoke(() =>
            {
                Button avatarButton = new();
                
                BitmapImage bi = new();
                bi.BeginInit();
                bi.UriSource = new(avatarPath, UriKind.Relative);
                bi.EndInit();
                Image image = new()
                {
                    Source = bi,
                    Width = 70,
                    Height = 70
                };
                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);
                avatarButton.Content = image;
                avatarButton.Command = ((AvatarSelectionPopupViewModel)DataContext).SelectAvatarCommand;
                avatarButton.CommandParameter = new object[] { this, image.Source.ToString() };
                avatarItemsControl.Items.Add(avatarButton);
            });

        }
    }
}
