using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TheExpanseRPG.UserControls
{
    /// <summary>
    /// Interaction logic for AttributeDescriptionBox.xaml
    /// </summary>
    public partial class AttributeDescriptionBox : UserControl
    {
        public AttributeDescriptionBox()
        {
            InitializeComponent();
        }
        // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register(nameof(HeaderContent), typeof(string), typeof(AttributeDescriptionBox), new PropertyMetadata(string.Empty));

        public string HeaderContent
        {
            get { return (string)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }


        public new string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(AttributeDescriptionBox), new PropertyMetadata(string.Empty));







        //public string Content
        //{
        //    get { return (string)GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ContentProperty =
        //    DependencyProperty.Register("Content", typeof(string), typeof(AttributeDescriptionBox), new PropertyMetadata(string.Empty()));



    }
}
