using System.Windows;
using System.Windows.Controls;

namespace TheExpanseRPG.UserControls
{
    public partial class WindowHeaderWithLogo : UserControl
    {
        public WindowHeaderWithLogo()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.IsNavigating = false;
            if (JustClose)
            {
                Window.GetWindow((DependencyObject)sender).Close();
            }
            else if (JustHide)
            {
                Window.GetWindow((DependencyObject)sender).Hide();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }


        public bool JustHide
        {
            get { return (bool)GetValue(JustHideProperty); }
            set { SetValue(JustHideProperty, value); }
        }

        public static readonly DependencyProperty JustHideProperty =
            DependencyProperty.Register(nameof(JustHide), typeof(bool), typeof(WindowHeaderWithLogo), new PropertyMetadata(false));

        public bool JustClose
        {
            get { return (bool)GetValue(JustCloseProperty); }
            set { SetValue(JustCloseProperty, value); }
        }

        public static readonly DependencyProperty JustCloseProperty =
            DependencyProperty.Register(nameof(JustClose), typeof(bool), typeof(WindowHeaderWithLogo), new PropertyMetadata(false));


    }
}
